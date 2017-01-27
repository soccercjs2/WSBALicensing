using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Web.Models
{
    public class PhoneNumbersVM
    {
        public string PrimaryPhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string FaxPhoneNumber { get; set; }
    }
}