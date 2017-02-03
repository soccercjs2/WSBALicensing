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
            return _donationWorker.GetDonations(license);
        }

        public void Confirm(ICollection<Donation> donations)
        {
            _donationWorker.Confirm(donations);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            ICollection<Donation> donations = GetDonations(license);

            if (donations == null)
            {
                return false;
            }

            bool complete = true;

            foreach (Donation donation in donations)
            {
                if (!donation.Confirmed)
                {
                    complete = false;
                }
            }

            return complete;
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
