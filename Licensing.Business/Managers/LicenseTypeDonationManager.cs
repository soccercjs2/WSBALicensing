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
    public class LicenseTypeDonationManager
    {
        private LicensingContext _context;
        private LicenseTypeDonationWorker _licenseTypeDonationWorker;

        public LicenseTypeDonationManager(LicensingContext context)
        {
            _context = context;
            _licenseTypeDonationWorker = new LicenseTypeDonationWorker(context);
        }

        public ICollection<LicenseTypeDonation> GetExcludedDonations(LicenseType licenseType)
        {
            List<LicenseTypeDonation> excluded = new List<LicenseTypeDonation>();
            ICollection<DonationProduct> donationProducts = _licenseTypeDonationWorker.GetProducts();

            foreach (DonationProduct donationProduct in donationProducts)
            {
                if (licenseType.LicenseTypeDonations == null)
                {
                    excluded.Add(new LicenseTypeDonation() { LicenseTypeId = licenseType.LicenseTypeId, Product = donationProduct });
                }

                bool foundMatch = false;

                foreach (LicenseTypeDonation licenseTypeDonation in licenseType.LicenseTypeDonations)
                {
                    foundMatch = licenseTypeDonation.Product.DonationProductId == donationProduct.DonationProductId;
                }

                if (!foundMatch)
                {
                    excluded.Add(new LicenseTypeDonation() { LicenseTypeId = licenseType.LicenseTypeId, Product = donationProduct });
                }
            }

            return excluded;
        }

        public void AddLicenseTypeDonation(LicenseType licenseType, LicenseTypeDonation licenseTypeDonation)
        {
            _licenseTypeDonationWorker.AddLicenseTypeDonation(licenseTypeDonation);
        }

        public void DeleteLicenseTypeDonation(LicenseType licenseType, LicenseTypeDonation licenseTypeDonation)
        {
            _licenseTypeDonationWorker.DeleteLicenseTypeDonation(licenseTypeDonation);
        }
    }
}
