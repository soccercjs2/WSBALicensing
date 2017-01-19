using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseTypeProduct
    {
        public int LicenseTypeProductId { get; set; }
        public int LicenseTypeId { get; set; }
        
        public LicensingProduct Product { get; set; }
        public int LicensingProductId { get; set; }
    }
}
