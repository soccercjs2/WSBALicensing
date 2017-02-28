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

        public SectionVM(License license, ICollection<SectionProduct> products)
        {
            LicenseId = license.LicenseId;

            Products = new List<SectionProductVM>();

            foreach (SectionProduct product in products)
            {
                SectionProductVM sectionProductVM = new SectionProductVM(product);

                if (license.Sections != null)
                {
                    foreach (Section section in license.Sections)
                    {
                        if (section.Product != null && section.Product.SectionProductId == product.SectionProductId)
                        {
                            sectionProductVM.PreSelected = true;
                            sectionProductVM.Selected = true;
                        }
                    }
                }

                Products.Add(sectionProductVM);
            }
        }
    }
}
