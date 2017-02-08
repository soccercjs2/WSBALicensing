using Licensing.Data.Context;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class AreaOfPracticeWorker
    {
        private LicensingContext _context;

        public AreaOfPracticeWorker(LicensingContext context)
        {
            _context = context;
        }
    }
}
