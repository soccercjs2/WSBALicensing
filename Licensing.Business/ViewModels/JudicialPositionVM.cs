using Licensing.Domain.Judicial;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class JudicialPositionVM
    {
        public int LicenseId { get; set; }
        public int SelectedOptionId { get; set; }
        public ICollection<JudicialPositionOption> Options { get; set; }

        public JudicialPositionVM() { }

        public JudicialPositionVM(License license, ICollection<JudicialPositionOption> options)
        {
            LicenseId = license.LicenseId;

            if (license.JudicialPosition != null && license.JudicialPosition.Option != null)
            {
                SelectedOptionId = license.JudicialPosition.Option.JudicialPositionOptionId;
            }

            Options = options;
        }
    }
}
