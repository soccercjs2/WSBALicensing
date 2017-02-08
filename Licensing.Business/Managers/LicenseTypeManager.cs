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
    public class LicenseTypeManager
    {
        private LicensingContext _context;
        private LicenseTypeWorker _licenseTypeWorker;

        public LicenseTypeManager(LicensingContext context)
        {
            _context = context;
            _licenseTypeWorker = new LicenseTypeWorker(context);
        }

        public bool IsComplete(License license)
        {
            return (license.LicenseType != null);
        }

        public LicenseType GetInactiveType()
        {
            return _licenseTypeWorker.GetLicenseType("Inactive Attorney");
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("LicenseType", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Membership Type",
                license.LicenseType.MembershipType,
                IsComplete(license),
                editRoute,
                null,
                null,
                "_MembershipType",
                license.LicenseType.Name
            );
        }
    }
}
