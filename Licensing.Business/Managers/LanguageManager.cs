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

        public ICollection<LanguageOption> GetOptions()
        {
            return _languageWorker.GetOptions();
        }

        public LanguageOption GetOption(int id)
        {
            return _languageWorker.GetOption(id);
        }

        public LanguageOption GetOption(string code)
        {
            return _languageWorker.GetOption(code);
        }

        public void AddLanguage(License license, int languageOptionId)
        {
            Language language = new Language();
            language.Option = GetOption(languageOptionId);

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

        public IList<LanguageOption> GetAmsOptions()
        {
            IList<LanguageOption> options = new List<LanguageOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetLanguageCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new LanguageOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(LanguageOption option)
        {
            if (option.LanguageOptionId == 0)
            {
                LanguageOption existingCode = _languageWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _languageWorker.SetOption(option);
        }

        public void DeleteOption(LanguageOption option)
        {
            _languageWorker.DeleteOption(option);
        }

        public IList<LanguageOption> GetCodesToBeAdded(ICollection<LanguageOption> codes, ICollection<LanguageOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<LanguageOption> GetCodesToBeActivated(ICollection<LanguageOption> codes, ICollection<LanguageOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<LanguageOption> GetCodesToBeChanged(ICollection<LanguageOption> codes, ICollection<LanguageOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<LanguageOption> GetCodesToBeDeactivated(ICollection<LanguageOption> codes, ICollection<LanguageOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<LanguageOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<LanguageOption> codesToDeactivate = new List<LanguageOption>();

            foreach (LanguageOption option in codesToRemove)
            {
                ICollection<Language> responsesWithOption = _languageWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<LanguageOption> GetCodesToBeDeleted(ICollection<LanguageOption> codes, ICollection<LanguageOption> amsCodes)
        {
            IList<LanguageOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<LanguageOption> codesToDeleted = new List<LanguageOption>();

            foreach (LanguageOption option in codesToRemove)
            {
                ICollection<Language> responsesWithOption = _languageWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Languages", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Additional Languages",
                license.LicenseType.Languages,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_Languages",
                GetLanguages(license)
            );
        }
    }
}
