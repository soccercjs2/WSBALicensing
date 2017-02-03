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
    public class SexualOrientationManager
    {
        private LicensingContext _context;
        private SexualOrientationWorker _sexualOrientationWorker;

        public SexualOrientationManager(LicensingContext context)
        {
            _context = context;
            _sexualOrientationWorker = new SexualOrientationWorker(context);
        }

        public void OptOut(License license)
        {
            _sexualOrientationWorker.OptOut(license);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return license.SexualOrientationOptedOut;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("SexualOrientation", "Edit", license.LicenseId);
            RouteContainer optOutRoute = new RouteContainer("SexualOrientation", "OptOut", license.LicenseId);

            return new DashboardContainerVM(
                "Sexual Orientation",
                license.LicenseType.SexualOrientation,
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
