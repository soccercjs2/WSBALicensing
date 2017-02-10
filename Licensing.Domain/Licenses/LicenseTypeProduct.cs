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

        [ForeignKey("LicenseProductId")]
        public virtual LicenseProduct Product { get; set; }
        public int? LicenseProductId { get; set; }
    }
}
