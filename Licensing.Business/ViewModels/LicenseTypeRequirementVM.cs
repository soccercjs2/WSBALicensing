using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeRequirementVM
    {
        public int LicenseTypeId { get; set; }
        public string Name { get; set; }
        public LicenseTypeRequirement LicenseTypeRequirement { get; set; }

        public LicenseTypeRequirementVM() { }

        public LicenseTypeRequirementVM(LicenseType licenseType)
        {
            LicenseTypeId = licenseType.LicenseTypeId;
            Name = licenseType.Name;
            LicenseTypeRequirement = licenseType.LicenseTypeRequirement;
        }
    }
}
