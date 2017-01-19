using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseType
    {
        public int LicenseTypeId { get; set; }
        public string Name { get; set; }
        
        public ICollection<LicenseTypeProduct> LicenseTypeProducts { get; set; }
    }
}
