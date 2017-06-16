using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class MembershipTypeVM
    {
        public int LicenseId { get; set; }

        public LicenseType CurrentLicenseType { get; set; }
        public LicenseType PreviousLicenseType { get; set; }
        public LicenseType SwitchableLicenseType { get; set; }

        public bool SwitchChecked { get; set; }
        public bool ResignChecked { get; set; }

        public MembershipTypeVM() { }

        public MembershipTypeVM(License license)
        {
            LicenseId = license.LicenseId;

            CurrentLicenseType = license.LicenseType;
            PreviousLicenseType = license.PreviousLicenseType;
            SwitchableLicenseType = license.LicenseType.SwitchableLicenseType;

            SwitchChecked = false;
            ResignChecked = false;
        }
    }
}
