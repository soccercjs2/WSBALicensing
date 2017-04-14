using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public LicensePeriod GetLicensePeriod(DateTime date)
        {
            return _context.LicensePeriods.Where(lp => lp.StartDate <= date && lp.EndDate >= date).FirstOrDefault();
        }

        public LicensePeriod GetLicensePeriod(int id)
        {
            return _context.LicensePeriods.Find(id);
        }

        public ICollection<LicensePeriod> GetLicensePeriods()
        {
            return _context.LicensePeriods.OrderBy(lp => lp.StartDate).ToList();
        }

        public void SetLicensePeriod(LicensePeriod licensePeriod)
        {
            _context.Entry(licensePeriod).State = licensePeriod.LicensePeriodId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
