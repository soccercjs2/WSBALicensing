using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseProductVM
    {
        public LicenseProduct LicenseProduct { get; set; }
        public bool PrimaryProduct { get; set; }
        public bool LateFeeProduct { get; set; }
        public decimal Price { get; set; }

        public LicenseProductVM() { }
        public LicenseProductVM(LicenseTypeProduct licenseTypeProduct, decimal price)
        {
            LicenseProduct = licenseTypeProduct.Product;
            Price = price;
            PrimaryProduct = licenseTypeProduct.PrimaryProduct;
            LateFeeProduct = licenseTypeProduct.LateFeeProduct;
        }
    }
}
