using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Languages;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class LanguageManager
    {
        private LicensingContext _context;
        private LanguageWorker _languageWorker;

        public LanguageManager(LicensingContext context)
        {
            _context = context;
            _languageWorker = new LanguageWorker(context);
        }

        public ICollection<Language> GetLanguages(License license)
        {
            return _languageWorker.GetLanguages(license);
        }
    }
}
