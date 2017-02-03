using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class EthnicityManager
    {
        private LicensingContext _context;
        private EthnicityWorker _ethnicityWorker;

        public EthnicityManager(LicensingContext context)
        {
            _context = context;
            _ethnicityWorker = new EthnicityWorker(context);
        }

        public void OptOut(License license)
        {
            _ethnicityWorker.OptOut(license);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return license.EthnicityOptedOut;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Ethnicity", "Edit", license.LicenseId);
            RouteContainer optOutRoute = new RouteContainer("Ethnicity", "OptOut", license.LicenseId);

            return new DashboardContainerVM(
                "Ethnicity/Race",
                license.LicenseType.Ethnicity,
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
