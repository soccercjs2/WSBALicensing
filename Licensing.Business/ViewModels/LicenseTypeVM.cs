using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeVM
    {
        public LicenseType LicenseType { get; set; }

        public LicenseTypeVM() { }

        public LicenseTypeVM(LicenseType licenseType)
        {
            LicenseType = licenseType;
        }
    }
}
