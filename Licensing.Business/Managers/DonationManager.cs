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

        public ICollection<DonationProduct> GetProducts()
        {
            return _donationWorker.GetProducts();
        }

        public DonationProduct GetProduct(int id)
        {
            return _donationWorker.GetProduct(id);
        }

        public DonationProduct GetProduct(string code)
        {
            return _donationWorker.GetProduct(code);
        }

        public void SetAmount(License license, int donationProductId, decimal amount)
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

        public IList<DonationProduct> GetAmsOptions()
        {
            IList<DonationProduct> options = new List<DonationProduct>();
            var codes = WSBA.AMS.CodeTypesManager.GetDonationProductCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new DonationProduct() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(DonationProduct option)
        {
            if (option.DonationProductId == 0)
            {
                DonationProduct existingCode = _donationWorker.GetProduct(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _donationWorker.SetOption(option);
        }

        public void DeleteOption(DonationProduct option)
        {
            _donationWorker.DeleteOption(option);
        }

        public IList<DonationProduct> GetCodesToBeAdded(ICollection<DonationProduct> codes, ICollection<DonationProduct> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<DonationProduct> GetCodesToBeActivated(ICollection<DonationProduct> codes, ICollection<DonationProduct> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<DonationProduct> GetCodesToBeChanged(ICollection<DonationProduct> codes, ICollection<DonationProduct> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<DonationProduct> GetCodesToBeDeactivated(ICollection<DonationProduct> codes, ICollection<DonationProduct> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<DonationProduct> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<DonationProduct> codesToDeactivate = new List<DonationProduct>();

            foreach (DonationProduct option in codesToRemove)
            {
                ICollection<Donation> responsesWithOption = _donationWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<DonationProduct> GetCodesToBeDeleted(ICollection<DonationProduct> codes, ICollection<DonationProduct> amsCodes)
        {
            IList<DonationProduct> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<DonationProduct> codesToDeleted = new List<DonationProduct>();

            foreach (DonationProduct option in codesToRemove)
            {
                ICollection<Donation> responsesWithOption = _donationWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Donation", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Donations",
                license.LicenseType.Donations,
                IsComplete(license),
                editRoute,
                null,
                true,
                "_Donations",
                GetDonations(license)
            );
        }
    }
}
