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
        public bool Flag { get; set; }

        public LicenseTypeProductVM() { }

        public LicenseTypeProductVM(LicenseTypeProduct licenseTypeProduct)
        {
            LicenseTypeProduct = licenseTypeProduct;
            Flag = false;
        }
    }
}
