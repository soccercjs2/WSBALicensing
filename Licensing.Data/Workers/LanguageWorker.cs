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

        public ICollection<Language> GetLanguages(License license)
        {
            if (license == null)
            {
                return null;
            }

            if (license.Languages == null)
            {
                return null;
            }

            ICollection<Language> languages = license.Languages;

            if (languages.Count == 0) { return null; }
            else { return languages; }
        }
    }
}
