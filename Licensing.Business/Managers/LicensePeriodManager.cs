using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class LicensePeriodManager
    {
        private LicensingContext _context;
        private LicensePeriodWorker _licensePeriodWorker;

        public LicensePeriodManager(LicensingContext context)
        {
            _context = context;
            _licensePeriodWorker = new LicensePeriodWorker(context);
        }

        public LicensePeriod GetCurrentLicensePeriod()
        {
            DateTime endDate = new DateTime(2018, 5, 1);

            return _licensePeriodWorker.GetLicensePeriod(endDate);
        }
    }
}
