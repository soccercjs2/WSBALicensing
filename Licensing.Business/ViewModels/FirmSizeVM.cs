using Licensing.Domain.FirmSizes;
using Licensing.Domain.Licenses;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class FirmSizeVM
    {
        public int LicenseId { get; set; }
        public int SelectedOptionId { get; set; }
        public ICollection<FirmSizeOption> Options { get; set; }

        public FirmSizeVM() { }

        public FirmSizeVM(License license, ICollection<FirmSizeOption> options)
        {
            LicenseId = license.LicenseId;

            if (license.FirmSize != null && license.FirmSize.Option != null)
            {
                SelectedOptionId = license.FirmSize.Option.FirmSizeOptionId;
            }

            Options = options;
        }
    }
}
