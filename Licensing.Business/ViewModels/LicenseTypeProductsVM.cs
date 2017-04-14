using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeProductsVM
    {
        public int LicenseTypeId { get; set; }
        public string Name { get; set; }
        public IList<LicenseTypeProductVM> IncludedProducts { get; set; }
        public IList<LicenseTypeProductVM> ExcludedProducts { get; set; }

        public LicenseTypeProductsVM() { }

        public LicenseTypeProductsVM(LicenseType licenseType, ICollection<LicenseTypeProduct> includedProducts, ICollection<LicenseTypeProduct> excludedProducts)
        {
            LicenseTypeId = licenseType.LicenseTypeId;
            Name = licenseType.Name;

            List<LicenseTypeProductVM> includedProductVMs = new List<LicenseTypeProductVM>();
            List<LicenseTypeProductVM> excludedProductVMs = new List<LicenseTypeProductVM>();

            foreach (LicenseTypeProduct licenseTypeProduct in includedProducts)
            {
                includedProductVMs.Add(new LicenseTypeProductVM(licenseTypeProduct));
            }

            foreach (LicenseTypeProduct licenseTypeProduct in excludedProducts)
            {
                excludedProductVMs.Add(new LicenseTypeProductVM(licenseTypeProduct));
            }

            IncludedProducts = includedProductVMs;
            ExcludedProducts = excludedProductVMs;
        }
    }
}
