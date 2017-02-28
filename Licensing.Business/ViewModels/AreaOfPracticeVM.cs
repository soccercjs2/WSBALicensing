using Licensing.Business.Tools;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AreaOfPracticeVM
    {
        public int LicenseId { get; set; }
        public List<AreaOfPracticeOptionVM> Options { get; set; }

        public AreaOfPracticeVM() { }

        public AreaOfPracticeVM(License license, ICollection<AreaOfPracticeOption> options)
        {
            LicenseId = license.LicenseId;

            Options = new List<AreaOfPracticeOptionVM>();

            foreach (AreaOfPracticeOption option in options)
            {
                AreaOfPracticeOptionVM areaOfPracticeOptionVM = new AreaOfPracticeOptionVM(option);

                if (license.AreasOfPractice != null)
                {
                    foreach (AreaOfPractice areaOfPractice in license.AreasOfPractice)
                    {
                        if (areaOfPractice.Option != null && areaOfPractice.Option.AreaOfPracticeOptionId == option.AreaOfPracticeOptionId)
                        {
                            areaOfPracticeOptionVM.PreSelected = true;
                            areaOfPracticeOptionVM.Selected = true;
                        }
                    }
                }

                Options.Add(areaOfPracticeOptionVM);
            }
        }
    }
}
