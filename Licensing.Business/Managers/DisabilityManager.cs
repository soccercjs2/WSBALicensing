using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class DisabilityManager
    {
        private LicensingContext _context;
        private DisabilityWorker _disabilityWorker;

        public DisabilityManager(LicensingContext context)
        {
            _context = context;
            _disabilityWorker = new DisabilityWorker(context);
        }

        public void OptOut(License license)
        {
            _disabilityWorker.OptOut(license);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return license.DisabilityOptedOut;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Disability", "Edit", license.LicenseId);
            RouteContainer optOutRoute = new RouteContainer("Disability", "OptOut", license.LicenseId);

            return new DashboardContainerVM(
                "Disability",
                license.LicenseType.Disability,
                IsComplete(license),
                editRoute,
                null,
                optOutRoute,
                null,
                null
            );
        }
    }
}
