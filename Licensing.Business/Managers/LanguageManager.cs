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
            RouteContainer editRoute = new RouteContainer("Language", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("Language", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Language",
                license.LicenseType.Languages,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_Languages",
                GetLanguages(license)
            );
        }
    }
}
