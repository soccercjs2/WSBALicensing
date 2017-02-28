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

        public ICollection<FirmSizeOption> GetOptions()
        {
            return _firmSizeWorker.GetOptions();
        }

        public void SetProfessionalLiabilityInsuranceOption(License license, int optionId)
        {
            FirmSizeOption option = _firmSizeWorker.GetOption(optionId);

            if (license.FirmSize == null)
            {
                license.FirmSize = new FirmSize();
            }

            license.FirmSize.Option = option;
            license.FirmSize.Confirmed = true;

            _context.SaveChanges();
        }

        public void Confirm(FirmSize firmSize)
        {
            firmSize.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
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
                false,
                "_FirmSize",
                license.FirmSize
            );
        }
    }
}
