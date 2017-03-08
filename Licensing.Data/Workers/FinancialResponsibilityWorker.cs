using Licensing.Data.Context;
using Licensing.Domain.FinancialResponsibilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class FinancialResponsibilityWorker
    {
        private LicensingContext _context;

        public FinancialResponsibilityWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<CoveredByOption> GetOptions()
        {
            return _context.CoveredByOptions.ToList();
        }

        public CoveredByOption GetOption(int id)
        {
            return _context.CoveredByOptions.Find(id);
        }

        public CoveredByOption GetOption(string code)
        {
            return _context.CoveredByOptions.Where(c => c.AmsCode == code).FirstOrDefault();
        }

        public void SetCoveredByOption(CoveredByOption coveredByOption)
        {
            _context.Entry(coveredByOption).State = coveredByOption.CoveredByOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
