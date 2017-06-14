using Licensing.Domain.Donations;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Licensing.Business.ViewModels
{
    public class CheckoutVM
    {
        public int LicenseId { get; set; }

        public IList<LicenseProductVM> LicenseProducts { get; set; }
        public IList<SectionProductVM> SectionProducts { get; set; }
        public IList<DonationProductVM> DonationProducts { get; set; }
        public decimal Total { get; set; }

        public RequirementType KellerRequirementType { get; set; }
        public bool HasKellerDeduction { get; set; }

        public RequirementType HardshipExemptionRequestRequirementType { get; set; }
        public bool HasHardshipExemptionRequest { get; set; }

        //credit card properties
        [Display(Name = "Credit Card Type")]
        public string CreditCardType { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }
        [Display(Name = "Expiration")]
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; }

        //eft properties
        [Display(Name = "Routing Number")]
        public string RoutingNumber { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        //dropdownlist properties
        public List<SelectListItem> CreditCardTypes { get; set; }
        public List<SelectListItem> ExpirationMonths { get; set; }
        public List<SelectListItem> ExpirationYears { get; set; }

        public CheckoutVM() { }
        public CheckoutVM(License license, IList<LicenseProductVM> licenseProducts, IList<SectionProductVM> sectionProducts, IList<DonationProductVM> donationProducts)
        {
            LicenseId = license.LicenseId;
            LicenseProducts = licenseProducts;
            SectionProducts = sectionProducts;
            DonationProducts = donationProducts;

            KellerRequirementType = license.LicenseType.LicenseTypeRequirement.KellerDeduction;
            HasKellerDeduction = license.KellerDeduction;

            HardshipExemptionRequestRequirementType = license.LicenseType.LicenseTypeRequirement.HardshipExemption;
            HasHardshipExemptionRequest = license.HardshipExemptionRequest != null;

            if (licenseProducts != null) { Total += licenseProducts.Sum(lp => lp.Price); }
            if (sectionProducts != null) { Total += sectionProducts.Sum(lp => lp.Price); }
            if (donationProducts != null) { Total += donationProducts.Sum(lp => lp.Amount); }

            CreditCardTypes = new List<SelectListItem>()
            {
                new SelectListItem{ Text="American Express", Value="AMEX" },
                new SelectListItem{ Text="MasterCard", Value="MC" },
                new SelectListItem{ Text="Visa", Value="VISA" }
            };

            ExpirationMonths = new List<SelectListItem>()
            {
                new SelectListItem{ Text="January", Value="1" },
                new SelectListItem{ Text="February", Value="2" },
                new SelectListItem{ Text="March", Value="3" },
                new SelectListItem{ Text="April", Value="4" },
                new SelectListItem{ Text="May", Value="5" },
                new SelectListItem{ Text="June", Value="6" },
                new SelectListItem{ Text="July", Value="7" },
                new SelectListItem{ Text="August", Value="8" },
                new SelectListItem{ Text="September", Value="9" },
                new SelectListItem{ Text="October", Value="10" },
                new SelectListItem{ Text="November", Value="11" },
                new SelectListItem{ Text="December", Value="12" }
            };

            ExpirationYears = new List<SelectListItem>();

            int currentYear = DateTime.Today.Year;
            for (int i = currentYear; i <= currentYear + 20; i++)
            {
                ExpirationYears.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
        }
    }
}
