using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class SectionVM
    {
        public int LicenseId { get; set; }
        public List<SectionProductVM> Products { get; set; }

        public SectionVM() { }

        public SectionVM(License license, ICollection<SectionProduct> products, ICollection<SectionProductVM> sectionProductsWithBalance)
        {
            LicenseId = license.LicenseId;

            Products = new List<SectionProductVM>();

            foreach (SectionProduct sectionProduct in products)
            {
                SectionProductVM sectionProductVM = new SectionProductVM(sectionProduct);

                if (sectionProductsWithBalance != null)
                {
                    foreach (SectionProductVM sectionProductWithBalance in sectionProductsWithBalance)
                    {
                        if (sectionProductWithBalance.SectionProductId == sectionProduct.SectionProductId)
                        {
                            sectionProductVM.PreSelected = true;
                            sectionProductVM.Selected = true;

                            if (sectionProductWithBalance.Price == 0) { sectionProductVM.Paid = true; }
                        }
                    }
                }

                Products.Add(sectionProductVM);
            }
        }
    }
}
