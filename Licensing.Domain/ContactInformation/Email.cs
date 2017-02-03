using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ContactInformation
{
    public class Email : Preloadable
    {
        public int EmailId { get; set; }
        public int LicenseId { get; set; }
        public string EmailAddress { get; set; }
    }
}
