﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Ethnicities
{
    public class EthnicityOption : Activatable
    {
        public int EthnicityOptionId { get; set; }
        public string Name { get; set; }
        public string AmsCode { get; set; }
    }
}
