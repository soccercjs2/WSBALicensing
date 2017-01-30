using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Judicial;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class JudicialPositionManager
    {
        private LicensingContext _context;
        private JudicialPositionWorker _judicialPositionWorker;

        public JudicialPositionManager(LicensingContext context)
        {
            _context = context;
            _judicialPositionWorker = new JudicialPositionWorker(context);
        }

        public void Confirm(JudicialPosition professionalLiabilityInsurance)
        {
            _judicialPositionWorker.Confirm(professionalLiabilityInsurance);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return (license.JudicialPosition != null && license.JudicialPosition.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("JudicialPosition", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("JudicialPosition", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Judicial Position",
                license.LicenseType.JudicialPosition,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_JudicialPosition",
                license.JudicialPosition
            );
        }
    }
}
