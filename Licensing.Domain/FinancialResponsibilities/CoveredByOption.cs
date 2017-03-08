using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.FinancialResponsibilities
{
    public class CoveredByOption : Activatable
    {
        public int CoveredByOptionId { get; set; }
        public string Name { get; set; }
        public string AmsCode { get; set; }
    }
}
