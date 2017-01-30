using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class ProfessionalLiabilityInsuranceWorker
    {
        private LicensingContext _context;

        public ProfessionalLiabilityInsuranceWorker(LicensingContext context)
        {
            _context = context;
        }

        public void Confirm(ProfessionalLiabilityInsurance professionalLiabilityInsurance)
        {
            professionalLiabilityInsurance.Confirmed = true;
            _context.SaveChanges();
        }
    }
}
