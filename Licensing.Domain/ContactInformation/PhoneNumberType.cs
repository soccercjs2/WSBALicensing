﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.ContactInformation
{
    public class PhoneNumberType : Activatable
    {
        public int PhoneNumberTypeId { get; set; }
        public string Name { get; set; }
    }
}
