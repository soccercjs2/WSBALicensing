using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Keller
{
    public class KellerDiscount : Activatable
    {
        public int KellerDiscountId { get; set; }
        public string Name { get; set; }
        public string AmsProductDiscountId { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
