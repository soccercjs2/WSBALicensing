using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class SwitchableLicenseTypeVM
    {
        public int LicenseTypeId { get; set; }
        public string LicenseTypeName { get; set; }
        public ICollection<LicenseType> OtherLicenseTypes { get; set; }
        public int SelectedLicenseTypeId { get; set; }

        public SwitchableLicenseTypeVM() { }

        public SwitchableLicenseTypeVM(LicenseType licenseType, ICollection<LicenseType> otherLicenseTypes)
        {
            LicenseTypeId = licenseType.LicenseTypeId;
            LicenseTypeName = licenseType.Name;
            OtherLicenseTypes = otherLicenseTypes;

            if (licenseType.SwitchableLicenseType != null)
            {
                SelectedLicenseTypeId = licenseType.SwitchableLicenseType.LicenseTypeId;
            }
        }
    }
}
