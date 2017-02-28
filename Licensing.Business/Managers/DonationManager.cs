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

        public Donation GetDonation(License license, int donationProductId)
        {
            if (license.Donations != null)
            {
                return license.Donations.Where(d => d.Product.DonationProductId == donationProductId).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public ICollection<DonationProduct> GetDonationProducts()
        {
            return _donationWorker.GetDonationProducts();
        }

        public DonationProduct GetDonationProduct(int id)
        {
            return _donationWorker.GetDonationProduct(id);
        }

        public void SetDonationAmount(License license, int donationProductId, decimal amount)
        {
            Donation donation = GetDonation(license, donationProductId);
            donation.Amount = amount;
            _context.SaveChanges();
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
                true,
                "_Donations",
                GetDonations(license)
            );
        }
    }
}
