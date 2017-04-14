using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeSectionVM
    {
        public LicenseTypeSection LicenseTypeSection { get; set; }
        public bool Flag { get; set; }

        public LicenseTypeSectionVM() { }

        public LicenseTypeSectionVM(LicenseTypeSection licenseTypeSection)
        {
            LicenseTypeSection = licenseTypeSection;
            Flag = false;
        }
    }
}
