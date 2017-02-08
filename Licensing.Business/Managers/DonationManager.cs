using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Donations;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class DonationManager
    {
        private LicensingContext _context;
        private DonationWorker _donationWorker;

        public DonationManager(LicensingContext context)
        {
            _context = context;
            _donationWorker = new DonationWorker(context);
        }

        public ICollection<Donation> GetDonations(License license)
        {
            if (license.Donations == null || license.Donations.Count == 0) { return null; }
            else { return license.Donations; }
        }

        public void Confirm(License license)
        {
            license.DonationsConfirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.DonationsConfirmed;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Donation", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("Donation", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Donations",
                license.LicenseType.Donations,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_Donations",
                GetDonations(license)
            );
        }
    }
}
