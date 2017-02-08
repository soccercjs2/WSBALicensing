using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class SectionWorker
    {
        private LicensingContext _context;

        public SectionWorker(LicensingContext context)
        {
            _context = context;
        }
    }
}
