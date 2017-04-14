using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ProfessionalLiabilityInsurances
{
    public class ProfessionalLiabilityInsuranceOption : Activatable
    {
        public int ProfessionalLiabilityInsuranceOptionId { get; set; }
        public string Description { get; set; }
        public bool? PrivatePractice { get; set; }
        public bool? CurrentlyInsured { get; set; }
        public bool? MaintainCoverage { get; set; }
    }
}
