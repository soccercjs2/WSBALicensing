using Licensing.Domain.ProfessionalLiabilityInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class ProfessionalLiabilityInsuranceOptionVM
    {
        public int ProfessionalLiabilityInsuranceOptionId { get; set; }
        public string Description { get; set; }
        public int PrivatePractice { get; set; }
        public int CurrentlyInsured { get; set; }
        public int MaintainCoverage { get; set; }

        public ProfessionalLiabilityInsuranceOptionVM() { }
        public ProfessionalLiabilityInsuranceOptionVM(ProfessionalLiabilityInsuranceOption option)
        {
            ProfessionalLiabilityInsuranceOptionId = option.ProfessionalLiabilityInsuranceOptionId;
            Description = option.Description;
            
            if (option.PrivatePractice == true) { PrivatePractice = 1; }
            else if (option.PrivatePractice == false) { PrivatePractice = 0; }
            else { PrivatePractice = -1; }

            if (option.CurrentlyInsured == true) { CurrentlyInsured = 1; }
            else if (option.CurrentlyInsured == false) { CurrentlyInsured = 0; }
            else { CurrentlyInsured = -1; }

            if (option.MaintainCoverage == true) { MaintainCoverage = 1; }
            else if (option.MaintainCoverage == false) { MaintainCoverage = 0; }
            else { MaintainCoverage = -1; }
        }
    }
}
