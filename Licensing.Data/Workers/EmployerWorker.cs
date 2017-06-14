using Licensing.Data.Context;
using Licensing.Domain.Employers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class EmployerWorker
    {
        private LicensingContext _context;

        public EmployerWorker(LicensingContext context)
        {
            _context = context;
        }

        public Employer GetEmployer(int masterCustomerId)
        {
            return _context.Employers.Where(e => e.AmsMasterCustomerId == masterCustomerId).FirstOrDefault();
        }
    }
}
