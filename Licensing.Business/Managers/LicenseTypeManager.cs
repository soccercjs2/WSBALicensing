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
    public class LicenseTypeManager
    {
        private LicensingContext _context;
        private LicenseTypeWorker _licenseTypeWorker;

        public LicenseTypeManager(LicensingContext context)
        {
            _context = context;
            _licenseTypeWorker = new LicenseTypeWorker(context);
        }

        public LicenseType GetLicenseType(int id)
        {
            return _licenseTypeWorker.GetLicenseType(id);
        }

        public LicenseType GetLicenseType(string type)
        {
            return _licenseTypeWorker.GetLicenseType(type);
        }

        public ICollection<LicenseType> GetLicenseTypes()
        {
            return _licenseTypeWorker.GetLicenseTypes();
        }

        public ICollection<LicenseType> GetOtherLicenseTypes(int id)
        {
            return _licenseTypeWorker.GetOtherLicenseTypes(id);
        }

        public bool IsComplete(License license)
        {
            return (license.LicenseType != null);
        }

        public void ChangePreviousLicenseType(License license, LicenseType licenseType)
        {
            license.PreviousLicenseType = licenseType;
            _context.SaveChanges();
        }

        public void ChangeLicenseType(License license, LicenseType licenseType)
        {
            license.LicenseType = licenseType;

            DonationManager donationManager = new DonationManager(_context);
            foreach (LicenseTypeDonation licenseTypeDonation in licenseType.LicenseTypeDonations)
            {
                Donation donation = donationManager.GetDonation(license, licenseTypeDonation.Product.DonationProductId);

                if (donation == null)
                {
                    donationManager.AddDonation(license, licenseTypeDonation.Product, licenseType.DefaultDonationAmount);
                }
            }

            _context.SaveChanges();
        }

        public void SetLicenseType(LicenseType licenseType)
        {
            _licenseTypeWorker.SetLicenseType(licenseType);
        }

        public void SetSwitchableLicenseType(LicenseType licenseType, int switchableLicenseTypeId)
        {
            licenseType.SwitchableLicenseTypeId = switchableLicenseTypeId;
            _context.SaveChanges();
        }

        public void SetDefaultDonationAmount(LicenseType licenseType, decimal defaultDonationAmount)
        {
            licenseType.DefaultDonationAmount = defaultDonationAmount;
            _context.SaveChanges();
        }

        public void SetLateFeePercentage(LicenseType licenseType, decimal lateFeePercentage)
        {
            licenseType.LateFeePercentage = lateFeePercentage;
            _context.SaveChanges();
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("MembershipType", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Membership Type",
                license.LicenseType.LicenseTypeRequirement.MembershipType,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_MembershipType",
                license.LicenseType.Name
            );
        }
    }
}
