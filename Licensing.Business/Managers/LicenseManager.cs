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
            return _licenseWorker.GetLicense(customer, licensePeriod);
        }

        public License GetLicenseByTrustAccount(int trustAccountId)
        {
            return _licenseWorker.GetLicenseWithTrustAccount(trustAccountId);
        }
    }
}
