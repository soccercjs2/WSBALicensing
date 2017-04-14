using Licensing.Data.Context;
using Licensing.Domain.Customers;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class LicenseWorker
    {
        private LicensingContext _context;

        public LicenseWorker(LicensingContext context)
        {
            _context = context;
        }

        public License GetLicense(int licenseId)
        {
            return _context.Licenses.Where(l => l.LicenseId == licenseId).FirstOrDefault();
        }

        public License GetLicense(Customer customer, LicensePeriod licensePeriod)
        {
            return _context.Licenses.Where(l => l.CustomerId == customer.CustomerId && l.LicensePeriod.LicensePeriodId == licensePeriod.LicensePeriodId).FirstOrDefault();
        }

        public License GetLicenseWithTrustAccount(int trustAccountId)
        {
            return _context.Licenses.Where(l => l.TrustAccount.TrustAccountId == trustAccountId).FirstOrDefault();
        }

        public ICollection<LicenseProduct> GetProducts()
        {
            return _context.LicenseProducts.OrderBy(o => o.Name).ToList();
        }

        public LicenseProduct GetProduct(int id)
        {
            return _context.LicenseProducts.Find(id);
        }

        public LicenseProduct GetProduct(string code, string amsBasisKey)
        {
            ICollection<LicenseProduct> options = _context.LicenseProducts.Where(c => c.AmsCode == code && c.AmsBasisKey == amsBasisKey).ToList();

            foreach (LicenseProduct option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<LicenseTypeProduct> GetResponsesWithOption(LicenseProduct option)
        {
            return _context.LicenseTypeProducts.Where(f => f.Product.LicenseProductId == option.LicenseProductId).ToList();
        }

        public void SetOption(LicenseProduct option)
        {
            _context.Entry(option).State = option.LicenseProductId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(LicenseProduct option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
