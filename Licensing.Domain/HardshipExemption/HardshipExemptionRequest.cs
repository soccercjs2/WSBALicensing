using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Hardship
{
    public class HardshipExemptionRequest
    {
        public int HardshipExemptionRequestId { get; set; }
        public decimal Income { get; set; }
        public int FamilySize { get; set; }
    }
}
