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
            ICollection<CoveredByOption> options = _context.CoveredByOptions.Where(c => c.AmsCode == code).ToList();

            foreach (CoveredByOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<FinancialResponsibility> GetResponsesWithOption(CoveredByOption option)
        {
            return _context.FinancialResponsibilities.Where(f => f.Option.CoveredByOptionId == option.CoveredByOptionId).ToList();
        }

        public void SetOption(CoveredByOption option)
        {
            _context.Entry(option).State = option.CoveredByOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(CoveredByOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
