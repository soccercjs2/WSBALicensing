using Licensing.Domain.ContactInformation;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class EmailVM
    {
        public int LicenseId { get; set; }
        public Email Email { get; set; }

        public EmailVM() { }
        public EmailVM(License license)
        {
            LicenseId = license.LicenseId;
            Email = license.Email;
        }
    }
}
