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

        public License GetCurrentLicense(Customer customer)
        {
            //get current license period
            LicensingPeriod licensingPeriod = _context.LicensingPeriods.Where(lp => lp.EndDate == new DateTime(2018, 5, 1)).FirstOrDefault();

            if (licensingPeriod == null)
            {
                return null;
            }

            //return license with specified customer and current licensing period
            return _context.Licenses.Where(l => l.CustomerId == customer.CustomerId && l.LicensingPeriod.LicensingPeriodId == licensingPeriod.LicensingPeriodId).FirstOrDefault();
        }
    }
}
