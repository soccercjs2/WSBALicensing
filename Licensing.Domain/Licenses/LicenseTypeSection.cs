using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseTypeSection
    {
        public int LicenseTypeSectionId { get; set; }
        public int LicenseTypeId { get; set; }

        [ForeignKey("SectionProductId")]
        public virtual SectionProduct Product { get; set; }
        public int? SectionProductId { get; set; }
    }
}
