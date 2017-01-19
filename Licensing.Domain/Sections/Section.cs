using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Sections
{
    public class Section
    {
        public int SectionId { get; set; }
        public int LicenseId { get; set; }
        
        public SectionProduct Product { get; set; }
        public int SectionProductId { get; set; }
    }
}
