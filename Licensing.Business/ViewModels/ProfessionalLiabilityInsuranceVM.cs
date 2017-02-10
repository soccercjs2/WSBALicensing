using Licensing.Domain.Licenses;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class ProfessionalLiabilityInsuranceVM
    {
        public int LicenseId { get; set; }
        public int SelectedOptionId { get; set; }
        public ICollection<ProfessionalLiabilityInsuranceOption> Options { get; set; }

        public ProfessionalLiabilityInsuranceVM() { }

        public ProfessionalLiabilityInsuranceVM(License license, ICollection<ProfessionalLiabilityInsuranceOption> options)
        {
            LicenseId = license.LicenseId;

            if (license.ProfessionalLiabilityInsurance != null && license.ProfessionalLiabilityInsurance.Option != null)
            {
                SelectedOptionId = license.ProfessionalLiabilityInsurance.Option.ProfessionalLiabilityInsuranceOptionId;
            }

            Options = options;
        }
    }
}
