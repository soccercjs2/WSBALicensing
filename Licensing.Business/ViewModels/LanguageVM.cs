using Licensing.Business.Tools;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Languages;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LanguageVM
    {
        public int LicenseId { get; set; }
        public List<LanguageOptionVM> Options { get; set; }

        public LanguageVM() { }

        public LanguageVM(License license, ICollection<LanguageOption> options)
        {
            LicenseId = license.LicenseId;

            Options = new List<LanguageOptionVM>();

            foreach (LanguageOption option in options)
            {
                LanguageOptionVM languageOptionVM = new LanguageOptionVM(option);

                if (license.Languages != null)
                {
                    foreach (Language language in license.Languages)
                    {
                        if (language.Option != null && language.Option.LanguageOptionId == option.LanguageOptionId)
                        {
                            languageOptionVM.PreSelected = true;
                            languageOptionVM.Selected = true;
                        }
                    }
                }

                Options.Add(languageOptionVM);
            }
        }
    }
}
