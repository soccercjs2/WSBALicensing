using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Genders;
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

        public ICollection<GenderOption> GetOptions()
        {
            return _genderWorker.GetOptions();
        }

        public void SetGenderOption(License license, int optionId)
        {
            GenderOption option = _genderWorker.GetOption(optionId);

            if (license.Gender == null)
            {
                license.Gender = new Gender();
            }

            license.Gender.Option = option;

            _context.SaveChanges();
        }

        public void OptOut(License license)
        {
            license.GenderOptedOut = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.GenderOptedOut || license.Gender != null;
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
                false,
                null,
                null
            );
        }
    }
}
