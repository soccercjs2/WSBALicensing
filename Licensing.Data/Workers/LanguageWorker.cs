using Licensing.Data.Context;
using Licensing.Domain.Languages;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ICollection<LanguageOption> GetOptions()
        {
            return _context.LanguageOptions.OrderBy(o => o.Name).ToList();
        }

        public LanguageOption GetOption(int id)
        {
            return _context.LanguageOptions.Find(id);
        }

        public void DeleteLanguage(Language language)
        {
            _context.Languages.Remove(language);
            _context.SaveChanges();
        }

        public LanguageOption GetOption(string code)
        {
            ICollection<LanguageOption> options = _context.LanguageOptions.Where(c => c.AmsCode == code).ToList();

            foreach (LanguageOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<Language> GetResponsesWithOption(LanguageOption option)
        {
            return _context.Languages.Where(f => f.Option.LanguageOptionId == option.LanguageOptionId).ToList();
        }

        public void SetOption(LanguageOption option)
        {
            _context.Entry(option).State = option.LanguageOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(LanguageOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
