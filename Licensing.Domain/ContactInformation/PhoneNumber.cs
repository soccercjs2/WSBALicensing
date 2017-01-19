using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ContactInformation
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public int LicenseId { get; set; }

        public PhoneNumberType PhoneNumberType { get; set; }
        public int PhoneNumberTypeId { get; set; }

        public int CountryCode { get; set; }
        public int AreaCode { get; set; }
        public int Number { get; set; }
    }
}
