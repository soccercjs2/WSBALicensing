using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Customers;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class MembershipProductManager
    {
        private LicensingContext _context;
        private MembershipProductWorker _membershipProductWorker;

        public MembershipProductManager(LicensingContext context)
        {
            _context = context;
            _membershipProductWorker = new MembershipProductWorker(context);
        }

        public ICollection<LicenseTypeProduct> GetLicenseTypeProducts(License license)
        {
            var licenseTypeProducts = license.LicenseType.LicenseTypeProducts;
            
            if (DateTime.Today < license.LicensePeriod.LateFeeDate || !NeedsLateFee(license))
            {
                licenseTypeProducts = licenseTypeProducts.Where(ltp => !ltp.LateFeeProduct).ToList();
            }

            return licenseTypeProducts;
        }

        public ICollection<LicenseProductVM> GetLicenseProductsWithPrices(License license)
        {
            var licenseTypeProducts = GetLicenseTypeProducts(license);

            return licenseTypeProducts.Select(ltp => new LicenseProductVM(ltp, GetPrice(license, ltp))).OrderByDescending(lpvm => lpvm.Price).ToList();
        }

        public ICollection<LicenseProductVM> GetLicenseProductsWithBalance(License license)
        {
            var licenseTypeProducts = GetLicenseTypeProducts(license);

            if (license.LicenseFeeExempt) { return null; }

            return licenseTypeProducts.Select(ltp => 
                new LicenseProductVM(ltp, GetBalance(license, ltp)))
                .Where(ltpvms => ltpvms.Price > 0)
                .OrderByDescending(lpvm => lpvm.Price).ToList();
        }

        public ICollection<LicenseProductVM> GetLicenseProductsWithPayment(License license)
        {
            var licenseTypeProducts = GetLicenseTypeProducts(license);

            return licenseTypeProducts.Select(ltp =>
                new LicenseProductVM(ltp, -GetTransactionTotal(license, ltp.Product)))
                .Where(ltpvms => ltpvms.Price > 0)
                .OrderByDescending(lpvm => lpvm.Price).ToList();
        }

        public decimal GetBalance(License license, LicenseTypeProduct licenseTypeProduct)
        {
            return GetBalance(license, licenseTypeProduct, null);
        }

        public decimal GetBalance(License license, LicenseTypeProduct licenseTypeProduct, DateTime? cutOffDate)
        {
            return GetPrice(license, licenseTypeProduct) + GetTransactionTotal(license, licenseTypeProduct.Product, cutOffDate);
        }

        public decimal GetPrice(License license, LicenseTypeProduct licenseTypeProduct)
        {
            return GetPrice(license, licenseTypeProduct, false);
        }

        public decimal GetPrice(License license, LicenseTypeProduct licenseTypeProduct, bool excludeKellerDeduction)
        {
            decimal price = 0;

            //if member has exemption, license fee product has zero price
            if (license.LicenseFeeExempt)
            {
                return Math.Round(price, 2);
            }
            //if license type product is a late fee product
            else if (licenseTypeProduct.LateFeeProduct)
            {
                //get the primary product for the license type
                var primaryProduct = GetPrimaryLicenseTypeProduct(license.LicenseType);

                //return the price of the primary product, and multiply by the late fee percentace for the license type
                if (primaryProduct != null) { price = GetPrice(license, primaryProduct, true) * license.LicenseType.LateFeePercentage / 100; }
            }
            else if (licenseTypeProduct.Product.Prices != null)
            {
                //calculate the number of days member admitted anywhere
                decimal daysAdmittedAnywhere = (decimal)(license.LicensePeriod.StartDate - license.Customer.EarliestAdmissionDate).TotalDays;

                //get the price that is between the price's basis parameters if it exists (basis values are in number of months, but working with days is easier (maybe?))
                var basisPrice = licenseTypeProduct.Product.Prices.Where(p => p.AmsBasisFrom != null && p.AmsBasisTo != null &&
                    ((decimal)p.AmsBasisFrom / 12 * 365) <= daysAdmittedAnywhere && ((decimal)p.AmsBasisTo / 12 * 365) > daysAdmittedAnywhere).FirstOrDefault();

                //return basis price if exists, otherwise get basisless price
                if (basisPrice != null) { price = basisPrice.Price; }
                else
                {
                    var defaultPrice = licenseTypeProduct.Product.Prices.Where(p => p.AmsBasisFrom == null && p.AmsBasisTo == null).FirstOrDefault();

                    if (defaultPrice != null) { price = defaultPrice.Price; }
                }
            }

            //if product is primary license product and member took keller deduction, update price
            if (!excludeKellerDeduction &&
                license.LicenseType.LicenseTypeRequirement.KellerDeduction != RequirementType.Excluded &&
                license.KellerDeduction == true && licenseTypeProduct.PrimaryProduct)
            {
                var kellerDeduction = license.LicenseType.KellerDiscount;

                if (kellerDeduction != null)
                {
                    decimal kellerDeductionAmount = price * kellerDeduction.DiscountPercentage / 100;
                    price -= kellerDeductionAmount;
                }
            }

            //return result rounded to 2 places
            return Math.Round(price, 2);
        }

        public decimal GetBalanceDue(License license)
        {
            var licenseProductVMs = GetLicenseProductsWithBalance(license);
            decimal balance = 0;

            foreach (var licenseProductVM in licenseProductVMs)
            {
                balance += licenseProductVM.Price;
            }

            return Math.Max(balance, 0);
        }

        public decimal GetTransactionTotal(License license, LicenseProduct licenseProduct)
        {
            return GetTransactionTotal(license, licenseProduct, null);
        }

        public decimal GetTransactionTotal(License license, LicenseProduct licenseProduct, DateTime? cutOffDate)
        {
            if (license.LicensingOrder == null || license.LicensingOrder.Transactions == null)
            {
                return 0;
            }

            var transactions = license.LicensingOrder.Transactions.Where(p => p.AmsCode == licenseProduct.AmsCode && (p.TransactionDate < cutOffDate || cutOffDate == null)).ToList();

            if (transactions == null) { return 0; }
            else
            {
                decimal transactionTotal = 0;
                foreach (var transaction in transactions)
                {
                    transactionTotal += transaction.Amount;
                }

                return transactionTotal;
            }
        }

        public LicenseTypeProduct GetPrimaryLicenseTypeProduct(LicenseType licenseType)
        {
            return licenseType.LicenseTypeProducts.Where(ltp => ltp.PrimaryProduct).FirstOrDefault();
        }

        public bool NeedsLateFee(License license)
        {
            //get license products that aren't a late fee
            var licenseTypeProducts = license.LicenseType.LicenseTypeProducts.Where(ltp => !ltp.LateFeeProduct).ToList();

            //get balance of non late fee license products
            decimal balance = licenseTypeProducts.Sum(ltp => GetBalance(license, ltp, license.LicensePeriod.LateFeeDate));

            //if has balance, needs late fee
            return (balance > 0);
        }

        public bool IsComplete(License license)
        {
            return GetBalanceDue(license) <= 0;
        }

        public void SetLicenseFeeExeption(License license, bool hasLicenseFeeExemption)
        {
            license.LicenseFeeExempt = hasLicenseFeeExemption;
            _context.SaveChanges();
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            return new DashboardContainerVM(
                "Membership Products",
                RequirementType.Optional,
                IsComplete(license),
                null,
                null,
                true,
                "_MembershipProducts",
                GetLicenseProductsWithPrices(license)
            );
        }
    }
}
