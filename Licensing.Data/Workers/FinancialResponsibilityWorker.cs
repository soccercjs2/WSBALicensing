using Licensing.Data.Context;
using Licensing.Domain.FinancialResponsibilities;
using System;
using System.Collections.Generic;
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
    }
}
