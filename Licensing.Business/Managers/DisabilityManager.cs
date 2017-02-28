using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Disabilities;
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

        public ICollection<DisabilityOption> GetOptions()
        {
            return _disabilityWorker.GetOptions();
        }

        public void SetDisabilityOption(License license, int optionId)
        {
            DisabilityOption option = _disabilityWorker.GetOption(optionId);

            if (license.Disability == null)
            {
                license.Disability = new Disability();
            }

            license.Disability.Option = option;

            _context.SaveChanges();
        }

        public void OptOut(License license)
        {
            license.DisabilityOptedOut = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.DisabilityOptedOut || license.Disability != null;
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
                false,
                null,
                null
            );
        }
    }
}
