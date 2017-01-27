using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class SectionWorker
    {
        private LicensingContext _context;
        private License _license;

        public SectionWorker(LicensingContext context, License license)
        {
            _context = context;
            _license = license;
        }

        public ICollection<Section> GetSections()
        {
            ICollection<Section> sections = _license.Sections;

            if (sections.Count == 0) { return null; }
            else { return sections; }
        }
    }
}
