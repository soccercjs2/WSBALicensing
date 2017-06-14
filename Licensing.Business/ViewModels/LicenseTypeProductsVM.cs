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
        public decimal LateFeePercentage { get; set; }
        public LicenseTypeProductVM PrimaryProduct { get; set; }
        public IList<LicenseTypeProductVM> OtherProducts { get; set; }
        public LicenseTypeProductVM LateFeeProduct { get; set; }
        public IList<LicenseTypeProductVM> ExcludedProducts { get; set; }

        public LicenseTypeProductsVM() { }

        public LicenseTypeProductsVM(LicenseType licenseType, ICollection<LicenseTypeProduct> includedProducts, ICollection<LicenseTypeProduct> excludedProducts)
        {
            LicenseTypeId = licenseType.LicenseTypeId;
            Name = licenseType.Name;
            LateFeePercentage = licenseType.LateFeePercentage;

            List<LicenseTypeProductVM> otherProductVMs = new List<LicenseTypeProductVM>();
            List<LicenseTypeProductVM> excludedProductVMs = new List<LicenseTypeProductVM>();

            foreach (LicenseTypeProduct licenseTypeProduct in includedProducts)
            {
                if (licenseTypeProduct.PrimaryProduct) { PrimaryProduct = new LicenseTypeProductVM(licenseTypeProduct); }
                else if (licenseTypeProduct.LateFeeProduct) { LateFeeProduct = new LicenseTypeProductVM(licenseTypeProduct); }
                else { otherProductVMs.Add(new LicenseTypeProductVM(licenseTypeProduct)); }
            }

            foreach (LicenseTypeProduct licenseTypeProduct in excludedProducts)
            {
                excludedProductVMs.Add(new LicenseTypeProductVM(licenseTypeProduct));
            }

            OtherProducts = otherProductVMs;
            ExcludedProducts = excludedProductVMs;
        }
    }
}
