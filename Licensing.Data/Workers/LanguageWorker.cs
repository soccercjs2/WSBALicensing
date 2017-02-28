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

        public ICollection<LanguageOption> GetLanguageOptions()
        {
            return _context.LanguageOptions.OrderBy(o => o.Name).ToList();
        }

        public LanguageOption GetLanguageOption(int id)
        {
            return _context.LanguageOptions.Find(id);
        }

        public void DeleteLanguage(Language language)
        {
            _context.Languages.Remove(language);
            _context.SaveChanges();
        }
    }
}
