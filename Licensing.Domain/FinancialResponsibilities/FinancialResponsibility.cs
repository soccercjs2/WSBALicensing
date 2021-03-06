﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.FinancialResponsibilities
{
    public class FinancialResponsibility : Preloadable
    {
        public int FinancialResponsibilityId { get; set; }
        public int AmsFinancialResponsibilityId { get; set; }
        public string Company { get; set; }
        public string PolicyNumber { get; set; }

        [ForeignKey("CoveredByOptionId")]
        public virtual CoveredByOption Option { get; set; }
        public int? CoveredByOptionId { get; set; }
    }
}
