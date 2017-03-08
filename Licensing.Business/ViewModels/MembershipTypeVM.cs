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
        public License License { get; set; }
        public int LicenseId { get; set; }
        public bool InactiveChecked { get; set; }
        public bool ResignChecked { get; set; }

        public MembershipTypeVM() { }

        public MembershipTypeVM(License license)
        {
            License = license;
            LicenseId = license.LicenseId;
            InactiveChecked = false;
            ResignChecked = false;
        }
    }
}
