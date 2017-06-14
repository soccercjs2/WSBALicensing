using Licensing.Domain.Donations;
using Licensing.Domain.Orders;
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
    public class ReceiptVM
    {
        public Order LicensingOrder { get; set; }
        public decimal LicensingTotal { get; set; }
        public Order SectionOrder { get; set; }
        public decimal SectionTotal { get; set; }
        public IList<LicenseProductVM> LicenseProducts { get; set; }
        public IList<SectionProductVM> SectionProducts { get; set; }
        public IList<DonationProductVM> DonationProducts { get; set; }

        public ReceiptVM() { }
        public ReceiptVM(Order licensingOrder, Order sectionOrder, IList<LicenseProductVM> licenseProducts, IList<SectionProductVM> sectionProducts, IList<DonationProductVM> donationProducts)
        {
            LicensingOrder = licensingOrder;
            SectionOrder = sectionOrder;
            LicenseProducts = licenseProducts;
            SectionProducts = sectionProducts;
            DonationProducts = donationProducts;

            if (licenseProducts != null) { LicensingTotal += licenseProducts.Sum(lp => lp.Price); }
            if (sectionProducts != null) { SectionTotal += sectionProducts.Sum(lp => lp.Price); }
            if (donationProducts != null) { LicensingTotal += donationProducts.Sum(lp => lp.Amount); }
        }
    }
}
