using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ProfessionalLiabilityInsurances
{
    public class ProfessionalLiabilityInsurance : Preloadable
    {
        public int ProfessionalLiabilityInsuranceId { get; set; }

        [ForeignKey("ProfessionalLiabilityInsuranceOptionId")]
        public virtual ProfessionalLiabilityInsuranceOption Option { get; set; }
        public int? ProfessionalLiabilityInsuranceOptionId { get; set; }

        public int AmsSequenceNumber { get; set; }
    }
}
