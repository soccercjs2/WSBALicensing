using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ContactInformation
{
    public class Email
    {
        public int EmailId { get; set; }
        public int LicenseId { get; set; }

        public EmailType EmailType { get; set; }
        public int EmailTypeId { get; set; }

        public string EmailAddress { get; set; }
    }
}
