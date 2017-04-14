using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeDonationsVM
    {
        public int LicenseTypeId { get; set; }
        public string Name { get; set; }
        public decimal DefaultDonationAmount { get; set; }
        public IList<LicenseTypeDonationVM> IncludedDonations { get; set; }
        public IList<LicenseTypeDonationVM> ExcludedDonations { get; set; }

        public LicenseTypeDonationsVM() { }

        public LicenseTypeDonationsVM(LicenseType licenseType, ICollection<LicenseTypeDonation> includedDonations, ICollection<LicenseTypeDonation> excludedDonations)
        {
            LicenseTypeId = licenseType.LicenseTypeId;
            Name = licenseType.Name;
            DefaultDonationAmount = licenseType.DefaultDonationAmount;

            List<LicenseTypeDonationVM> includedDonationVMs = new List<LicenseTypeDonationVM>();
            List<LicenseTypeDonationVM> excludedDonationVMs = new List<LicenseTypeDonationVM>();

            foreach (LicenseTypeDonation licenseTypeDonation in includedDonations)
            {
                includedDonationVMs.Add(new LicenseTypeDonationVM(licenseTypeDonation));
            }

            foreach (LicenseTypeDonation licenseTypeDonation in excludedDonations)
            {
                excludedDonationVMs.Add(new LicenseTypeDonationVM(licenseTypeDonation));
            }

            IncludedDonations = includedDonationVMs;
            ExcludedDonations = excludedDonationVMs;
        }
    }
}
