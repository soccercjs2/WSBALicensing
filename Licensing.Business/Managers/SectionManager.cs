using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class SectionManager
    {
        private LicensingContext _context;
        private SectionWorker _sectionWorker;

        public SectionManager(LicensingContext context)
        {
            _context = context;
            _sectionWorker = new SectionWorker(context);
        }

        public ICollection<Section> GetSections(License license)
        {
            return _sectionWorker.GetSections(license);
        }
    }
}
