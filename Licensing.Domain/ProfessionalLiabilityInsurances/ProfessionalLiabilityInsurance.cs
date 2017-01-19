using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ProfessionalLiabilityInsurances
{
    public class ProfessionalLiabilityInsurance
    {
        public int ProfessionalLiabilityInsuranceId { get; set; }
        
        public ProfessionalLiabilityInsuranceOption Option { get; set; }
        public int ProfessionalLiabilityInsuranceOptionId { get; set; }
    }
}
