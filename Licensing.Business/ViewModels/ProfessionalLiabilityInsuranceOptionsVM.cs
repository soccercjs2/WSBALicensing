using Licensing.Domain.ProfessionalLiabilityInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class ProfessionalLiabilityInsuranceOptionsVM
    {
        public ICollection<ProfessionalLiabilityInsuranceOption> Options { get; set; }
    }
}
