using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Donations;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class DonationProductVM
    {
        public int DonationProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool Donating { get; set; }

        public DonationProductVM() { }

        public DonationProductVM(Donation donation)
        {
            DonationProductId = donation.Product.DonationProductId;
            Name = donation.Product.Name;
            Description = donation.Product.Description;
            Amount = donation.Amount;

            Donating = (donation.Amount > 0);
        }
    }
}
