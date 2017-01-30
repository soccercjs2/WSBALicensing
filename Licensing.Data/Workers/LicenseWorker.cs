using Licensing.Data.Context;
using Licensing.Domain.Customers;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
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

        public License GetLicense(Customer customer, LicensingPeriod licensingPeriod)
        {
            return customer.Licenses.Where(l => l.LicensingPeriodId == licensingPeriod.LicensingPeriodId).FirstOrDefault();
        }
    }
}
