using Licensing.Data.Context;
using Licensing.Domain.Languages;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class LanguageWorker
    {
        private LicensingContext _context;

        public LanguageWorker(LicensingContext context)
        {
            _context = context;
        }
    }
}
