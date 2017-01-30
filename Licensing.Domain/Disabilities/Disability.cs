﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Disabilities
{
    public class Disability : OptOutable
    {
        public int DisabilityId { get; set; }
        
        public virtual DisabilityOption Option { get; set; }
        public int DisabilityOptionId { get; set; }
    }
}
