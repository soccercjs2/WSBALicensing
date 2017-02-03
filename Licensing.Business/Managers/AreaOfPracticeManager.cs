using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class AreaOfPracticeManager
    {
        private LicensingContext _context;
        private AreaOfPracticeWorker _areaOfPracticeWorker;

        public AreaOfPracticeManager(LicensingContext context)
        {
            _context = context;
            _areaOfPracticeWorker = new AreaOfPracticeWorker(context);
        }

        public ICollection<AreaOfPractice> GetAreasOfPractice(License license)
        {
            return _areaOfPracticeWorker.GetAreasOfPractice(license);
        }

        public void Confirm(License license)
        {
            _areaOfPracticeWorker.Confirm(license);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return license.AreasOfPracticeConfirmed;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("AreaOfPractice", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("AreaOfPractice", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Areas of Practice",
                license.LicenseType.AreasOfPractice,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_AreasOfPractice",
                GetAreasOfPractice(license)
            );
        }
    }
}
