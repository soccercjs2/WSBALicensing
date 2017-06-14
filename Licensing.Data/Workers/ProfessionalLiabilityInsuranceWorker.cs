using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void DeleteProfessionalLiabilityInsurance(ProfessionalLiabilityInsurance professionalLiabilityInsurance)
        {
            _context.ProfessionalLiabilityInsurances.Remove(professionalLiabilityInsurance);
            _context.SaveChanges();
        }

        public ICollection<ProfessionalLiabilityInsuranceOption> GetOptions()
        {
            return _context.ProfessionalLiabilityInsuranceOptions.OrderBy(o => o.ProfessionalLiabilityInsuranceOptionId).ToList();
        }

        public ProfessionalLiabilityInsuranceOption GetOption(int id)
        {
            return _context.ProfessionalLiabilityInsuranceOptions.Find(id);
        }

        public ProfessionalLiabilityInsuranceOption GetOption(bool? privatePractice, bool? currentlyInsured, bool? maintainCoverage)
        {
            return _context.ProfessionalLiabilityInsuranceOptions.Where(o => 
                (o.PrivatePractice == privatePractice || (o.PrivatePractice == null && privatePractice == null)) &&
                (o.CurrentlyInsured == currentlyInsured || (o.CurrentlyInsured == null && currentlyInsured == null)) &&
                (o.MaintainCoverage == maintainCoverage || (o.MaintainCoverage == null && maintainCoverage == null))).FirstOrDefault();
        }

        public void SetOption(ProfessionalLiabilityInsuranceOption option)
        {
            _context.Entry(option).State = option.ProfessionalLiabilityInsuranceOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
