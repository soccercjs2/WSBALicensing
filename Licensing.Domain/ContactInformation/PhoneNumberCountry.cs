using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ContactInformation
{
    public class PhoneNumberCountry : Activatable
    {
        public int PhoneNumberCountryId { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string InternationalCode { get; set; }
    }
}
