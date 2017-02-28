using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Donations;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class DonationVM
    {
        public int LicenseId { get; set; }
        public List<DonationProductVM> Products { get; set; }

        public DonationVM() { }

        public DonationVM(License license)
        {
            LicenseId = license.LicenseId;

            Products = new List<DonationProductVM>();

            foreach (Donation donation in license.Donations)
            {
                Products.Add(new DonationProductVM(donation));
            }
        }
    }
}
