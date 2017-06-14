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
        public string AmsCode { get; set; }
        public int AmsProductId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool Donating { get; set; }

        public DonationProductVM() { }

        public DonationProductVM(Donation donation)
        {
            DonationProductId = donation.Product.DonationProductId;
            Name = donation.Product.Name;
            AmsCode = donation.Product.AmsCode;
            AmsProductId = donation.Product.AmsProductId;
            Description = donation.Product.Description;
            Amount = donation.Amount;

            Donating = (donation.Amount > 0);
        }

        public DonationProductVM(Donation donation, decimal balance)
        {
            DonationProductId = donation.Product.DonationProductId;
            Name = donation.Product.Name;
            AmsCode = donation.Product.AmsCode;
            AmsProductId = donation.Product.AmsProductId;
            Description = donation.Product.Description;
            Amount = balance;

            Donating = (donation.Amount > 0);
        }
    }
}
