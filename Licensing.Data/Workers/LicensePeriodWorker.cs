using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class LicensePeriodWorker
    {
        private LicensingContext _context;

        public LicensePeriodWorker(LicensingContext context)
        {
            _context = context;
        }

        public LicensePeriod GetLicensePeriod(DateTime endDate)
        {
            return _context.LicensePeriods.Where(lp => lp.EndDate == endDate).FirstOrDefault();
        }
    }
}
