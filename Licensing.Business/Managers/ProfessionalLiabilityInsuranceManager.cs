using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using Licensing.Business.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class ProfessionalLiabilityInsuranceManager
    {
        private LicensingContext _context;
        private ProfessionalLiabilityInsuranceWorker _professionalLiabilityInsuranceWorker;

        public ProfessionalLiabilityInsuranceManager(LicensingContext context)
        {
            _context = context;
            _professionalLiabilityInsuranceWorker = new ProfessionalLiabilityInsuranceWorker(context);
        }

        public ICollection<ProfessionalLiabilityInsuranceOption> GetOptions()
        {
            return _professionalLiabilityInsuranceWorker.GetOptions();
        }

        public void SetProfessionalLiabilityInsuranceOption(License license, int optionId)
        {
            ProfessionalLiabilityInsuranceOption option = _professionalLiabilityInsuranceWorker.GetOption(optionId);

            if (license.ProfessionalLiabilityInsurance == null)
            {
                license.ProfessionalLiabilityInsurance = new ProfessionalLiabilityInsurance();
            }

            license.ProfessionalLiabilityInsurance.Option = option;
            license.ProfessionalLiabilityInsurance.Confirmed = true;            

            _context.SaveChanges();
        }

        public void Confirm(License license)
        {
            license.ProfessionalLiabilityInsurance.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.ProfessionalLiabilityInsurance != null && license.ProfessionalLiabilityInsurance.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("ProfessionalLiabilityInsurance", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Professional Liability Insurance",
                license.LicenseType.ProfessionalLiabilityInsurance,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_ProfessionalLiabilityInsurance",
                license.ProfessionalLiabilityInsurance
            );
        }
    }
}
