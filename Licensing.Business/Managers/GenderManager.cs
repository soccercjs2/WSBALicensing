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
    public class GenderManager
    {
        private LicensingContext _context;
        private GenderWorker _genderWorker;

        public GenderManager(LicensingContext context)
        {
            _context = context;
            _genderWorker = new GenderWorker(context);
        }

        public void OptOut(License license)
        {
            _genderWorker.OptOut(license);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return license.GenderOptedOut;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Gender", "Edit", license.LicenseId);
            RouteContainer optOutRoute = new RouteContainer("Gender", "OptOut", license.LicenseId);

            return new DashboardContainerVM(
                "Gender",
                license.LicenseType.Gender,
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
