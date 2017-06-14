using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Customers;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class LicenseManager
    {
        private LicensingContext _context;
        private LicenseWorker _licenseWorker;

        public LicenseManager(LicensingContext context)
        {
            _context = context;
            _licenseWorker = new LicenseWorker(context);
        }

        public License GetLicense(int licenseId)
        {
            //return address
            return _licenseWorker.GetLicense(licenseId);
        }

        public License GetLicense(Customer customer, LicensePeriod licensePeriod)
        {
            AmsUpdateManager amsUpdateManager = new AmsUpdateManager(_context);

            License license = _licenseWorker.GetLicense(customer, licensePeriod);

            if (license == null)
            {
                license = new License();
                license.LicensePeriod = licensePeriod;
                license.Customer = customer;

                if (customer.Licenses == null)
                {
                    customer.Licenses = new List<License>();
                }

                customer.Licenses.Add(license);
            }

            amsUpdateManager.UpdateLicense(ref license);
            amsUpdateManager.UpdateOrders(ref license);

            return license;
        }

        public License GetLicenseByTrustAccount(int trustAccountId)
        {
            return _licenseWorker.GetLicenseWithTrustAccount(trustAccountId);
        }

        public ICollection<LicenseProduct> GetProducts()
        {
            return _licenseWorker.GetProducts();
        }

        public LicenseProduct GetProduct(int id)
        {
            return _licenseWorker.GetProduct(id);
        }

        public LicenseProduct GetProduct(string code)
        {
            return _licenseWorker.GetProduct(code);
        }

        public void SetLastAmsUpdate(License license, DateTime lastAmsUpdate)
        {
            license.LastAmsUpdate = lastAmsUpdate;
            _context.SaveChanges();
        }

        public IList<LicenseProduct> GetAmsOptions()
        {
            IList<LicenseProduct> options = new List<LicenseProduct>();
            var codes = WSBA.AMS.CodeTypesManager.GetLicenseProductCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new LicenseProduct() { Name = code.Description, AmsCode = code.Code, AmsProductId = code.ProductId, Active = true });
            }

            return options;
        }

        public void SetOption(LicenseProduct option)
        {
            if (option.LicenseProductId == 0)
            {
                LicenseProduct existingCode = _licenseWorker.GetProduct(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _licenseWorker.SetOption(option);
        }

        public void DeleteOption(LicenseProduct option)
        {
            _licenseWorker.DeleteOption(option);
        }

        public IList<LicenseProduct> GetCodesToBeAdded(ICollection<LicenseProduct> codes, ICollection<LicenseProduct> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<LicenseProduct> GetCodesToBeActivated(ICollection<LicenseProduct> codes, ICollection<LicenseProduct> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<LicenseProduct> GetCodesToBeChanged(ICollection<LicenseProduct> codes, ICollection<LicenseProduct> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<LicenseProduct> GetCodesToBeDeactivated(ICollection<LicenseProduct> codes, ICollection<LicenseProduct> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<LicenseProduct> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();

            IList<LicenseProduct> codesToDeactivate = new List<LicenseProduct>();

            foreach (LicenseProduct option in codesToRemove)
            {
                ICollection<LicenseTypeProduct> responsesWithOption = _licenseWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<LicenseProduct> GetCodesToBeDeleted(ICollection<LicenseProduct> codes, ICollection<LicenseProduct> amsCodes)
        {
            IList<LicenseProduct> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();

            IList<LicenseProduct> codesToDeleted = new List<LicenseProduct>();

            foreach (LicenseProduct option in codesToRemove)
            {
                ICollection<LicenseTypeProduct> responsesWithOption = _licenseWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public ICollection<LicenseProductPrice> GetPrices()
        {
            return _licenseWorker.GetPrices();
        }

        public void SetPrice(LicenseProductPrice price)
        {
            _licenseWorker.SetPrice(price);
        }

        public void DeletePrice(LicenseProductPrice price)
        {
            _licenseWorker.DeletePrice(price);
        }

        public IList<LicenseProductPrice> GetPricesToBeAdded(ICollection<LicenseProduct> codes)
        {
            IList <LicenseProductPrice> pricesToBeAdded = new List<LicenseProductPrice>();

            foreach (LicenseProduct product in codes)
            {
                var amsPrices = WSBA.AMS.CodeTypesManager.GetProductPricingList(product.AmsCode);
                var prices = _licenseWorker.GetPrices(product);

                foreach (var amsPrice in amsPrices)
                {
                    var foundPrice = prices.Where(p => (p.AmsBasisFrom == amsPrice.MinBasis || (p.AmsBasisFrom == null && amsPrice.MinBasis == null)) &&
                        (p.AmsBasisTo == amsPrice.MaxBasis || (p.AmsBasisTo == null && amsPrice.MaxBasis == null))).FirstOrDefault();

                    if (foundPrice == null)
                    {
                        pricesToBeAdded.Add(new LicenseProductPrice()
                        {
                            LicenseProductId = product.LicenseProductId,
                            Price = amsPrice.Price,
                            AmsBasisFrom = amsPrice.MinBasis,
                            AmsBasisTo = amsPrice.MaxBasis
                        });
                    }
                }
            }

            return pricesToBeAdded;
        }

        public IList<LicenseProductPrice> GetPricesToBeChanged(ICollection<LicenseProduct> codes)
        {
            IList<LicenseProductPrice> pricesToBeChanged = new List<LicenseProductPrice>();

            foreach (LicenseProduct product in codes)
            {
                var amsPrices = WSBA.AMS.CodeTypesManager.GetProductPricingList(product.AmsCode);
                var prices = _licenseWorker.GetPrices(product);

                foreach (var amsPrice in amsPrices)
                {
                    var foundPrice = prices.Where(p => (p.AmsBasisFrom == amsPrice.MinBasis || (p.AmsBasisFrom == null && amsPrice.MinBasis == null)) &&
                        (p.AmsBasisTo == amsPrice.MaxBasis || (p.AmsBasisTo == null && amsPrice.MaxBasis == null)) &&
                        p.Price != amsPrice.Price).FirstOrDefault();

                    if (foundPrice != null)
                    {
                        foundPrice.Price = amsPrice.Price;
                        pricesToBeChanged.Add(foundPrice);
                    }
                }
            }

            return pricesToBeChanged;
        }

        public IList<LicenseProductPrice> GetPricesToBeDeleted(ICollection<LicenseProduct> codes)
        {
            IList<LicenseProductPrice> pricesToBeDeleted = new List<LicenseProductPrice>();
            var prices = _licenseWorker.GetPrices();

            foreach (var price in prices)
            {
                var foundProduct = codes.Where(c => c.LicenseProductId == price.LicenseProductId).FirstOrDefault();

                if (foundProduct == null)
                {
                    pricesToBeDeleted.Add(price);
                }
            }

            return pricesToBeDeleted;
        }
    }
}
