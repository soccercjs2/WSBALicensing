﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ContactInformation
{
    public class PhoneNumber : Preloadable
    {
        public int PhoneNumberId { get; set; }
        public int LicenseId { get; set; }

        [ForeignKey("PhoneNumberTypeId")]
        public virtual PhoneNumberType PhoneNumberType { get; set; }
        public int? PhoneNumberTypeId { get; set; }

        public int CountryCode { get; set; }
        public int AreaCode { get; set; }
        public int ExchangeCode { get; set; }
        public int LineNumber { get; set; }
    }
}
