using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeProductVM
    {
        public LicenseTypeProduct LicenseTypeProduct { get; set; }
        public bool Primary { get; set; }
        public bool Other { get; set; }
        public bool LateFee { get; set; }
        public bool Delete { get; set; }

        public LicenseTypeProductVM() { }

        public LicenseTypeProductVM(LicenseTypeProduct licenseTypeProduct)
        {
            LicenseTypeProduct = licenseTypeProduct;
        }
    }
}
