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

        public void Confirm(FinancialResponsibility financialResponsibility)
        {
            financialResponsibility.Confirmed = true;
            _context.SaveChanges();
        }
    }
}
