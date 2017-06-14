using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseProductPrice
    {
        public int LicenseProductPriceId { get; set; }
        public int LicenseProductId { get; set; }
        public decimal? AmsBasisFrom { get; set; }
        public decimal? AmsBasisTo { get; set; }
        public decimal Price { get; set; }
    }
}
