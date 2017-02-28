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

        public SectionWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<SectionProduct> GetSectionProducts()
        {
            return _context.SectionProducts.OrderBy(o => o.Name).ToList();
        }

        public SectionProduct GetSectionProduct(int id)
        {
            return _context.SectionProducts.Find(id);
        }

        public void DeleteSection(Section section)
        {
            _context.Sections.Remove(section);
            _context.SaveChanges();
        }
    }
}
