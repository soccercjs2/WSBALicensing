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
        private License _license;

        public LanguageWorker(LicensingContext context, License license)
        {
            _context = context;
            _license = license;
        }

        public ICollection<Language> GetLanguages()
        {
            ICollection<Language> languages = _license.Languages;

            if (languages.Count == 0) { return null; }
            else { return languages; }
        }
    }
}
