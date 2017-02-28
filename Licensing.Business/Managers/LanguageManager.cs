using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
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
            if (license.Languages == null || license.Languages.Count == 0) { return null; }
            else { return license.Languages; }
        }

        public ICollection<LanguageOption> GetLanguageOptions()
        {
            return _languageWorker.GetLanguageOptions();
        }

        public LanguageOption GetLanguageOption(int id)
        {
            return _languageWorker.GetLanguageOption(id);
        }

        public void AddLanguage(License license, int languageOptionId)
        {
            Language language = new Language();
            language.Option = GetLanguageOption(languageOptionId);

            license.Languages.Add(language);

            _context.SaveChanges();
        }

        public void DeleteLanguage(License license, int languageOptionId)
        {
            Language language = license.Languages.Where(a => a.Option.LanguageOptionId == languageOptionId).FirstOrDefault();
            _languageWorker.DeleteLanguage(language);

            _context.SaveChanges();
        }

        public void Confirm(License license)
        {
            license.LanguagesConfirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.LanguagesConfirmed;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Languages", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("Languages", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Additional Languages",
                license.LicenseType.Languages,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                false,
                "_Languages",
                GetLanguages(license)
            );
        }
    }
}
