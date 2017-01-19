using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicensingProduct
    {
        public int LicensingProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string AmsCode { get; set; }
    }
}