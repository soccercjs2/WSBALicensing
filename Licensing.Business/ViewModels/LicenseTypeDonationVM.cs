using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeDonationVM
    {
        public LicenseTypeDonation LicenseTypeDonation { get; set; }
        public bool Flag { get; set; }

        public LicenseTypeDonationVM() { }

        public LicenseTypeDonationVM(LicenseTypeDonation licenseTypeDonation)
        {
            LicenseTypeDonation = licenseTypeDonation;
            Flag = false;
        }
    }
}
