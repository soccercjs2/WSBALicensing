using Licensing.Business.Tools;
using Licensing.Domain.Licenses;
using Licensing.Domain.PracticeAreas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class PracticeAreaVM
    {
        public int LicenseId { get; set; }
        public List<PracticeAreaOptionVM> Options { get; set; }

        public PracticeAreaVM() { }

        public PracticeAreaVM(License license, ICollection<PracticeAreaOption> options)
        {
            LicenseId = license.LicenseId;

            Options = new List<PracticeAreaOptionVM>();

            foreach (PracticeAreaOption option in options)
            {
                PracticeAreaOptionVM practiceAreaOptionVM = new PracticeAreaOptionVM(option);

                if (license.PracticeAreas != null)
                {
                    foreach (PracticeArea practiceArea in license.PracticeAreas)
                    {
                        if (practiceArea.Option != null && practiceArea.Option.PracticeAreaOptionId == option.PracticeAreaOptionId)
                        {
                            practiceAreaOptionVM.PreSelected = true;
                            practiceAreaOptionVM.Selected = true;
                        }
                    }
                }

                Options.Add(practiceAreaOptionVM);
            }
        }
    }
}
