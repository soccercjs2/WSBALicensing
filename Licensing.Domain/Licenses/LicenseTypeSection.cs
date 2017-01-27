using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseTypeSection
    {
        public int LicenseTypeSectionId { get; set; }
        public int LicenseTypeId { get; set; }

        public virtual SectionProduct Product { get; set; }
        public int SectionProductId { get; set; }
    }
}
