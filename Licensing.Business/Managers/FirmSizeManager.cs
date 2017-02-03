using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.FirmSizes;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class FirmSizeManager
    {
        private LicensingContext _context;
        private FirmSizeWorker _firmSizeWorker;

        public FirmSizeManager(LicensingContext context)
        {
            _context = context;
            _firmSizeWorker = new FirmSizeWorker(context);
        }

        public void Confirm(FirmSize trustAccount)
        {
            _firmSizeWorker.Confirm(trustAccount);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return (license.FirmSize != null && license.FirmSize.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("FirmSize", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("FirmSize", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Firm Size",
                license.LicenseType.FirmSize,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_FirmSize",
                license.FirmSize
            );
        }
    }
}
