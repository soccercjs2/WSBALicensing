using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseProduct : Activatable
    {
        public int LicenseProductId { get; set; }
        public string Name { get; set; }
        public string AmsCode { get; set; }
        public int AmsProductId { get; set; }

        public virtual ICollection<LicenseProductPrice> Prices { get; set; }
    }
}