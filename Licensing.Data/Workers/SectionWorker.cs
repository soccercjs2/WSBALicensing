using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ICollection<SectionProduct> GetProducts()
        {
            return _context.SectionProducts.OrderBy(o => o.Name).ToList();
        }

        public SectionProduct GetProduct(int id)
        {
            return _context.SectionProducts.Find(id);
        }

        public void DeleteSection(Section section)
        {
            _context.Sections.Remove(section);
            _context.SaveChanges();
        }

        public SectionProduct GetProduct(string code)
        {
            ICollection<SectionProduct> options = _context.SectionProducts.Where(c => c.AmsCode == code).ToList();

            foreach (SectionProduct option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<Section> GetResponsesWithOption(SectionProduct option)
        {
            return _context.Sections.Where(f => f.Product.SectionProductId == option.SectionProductId).ToList();
        }

        public void SetOption(SectionProduct option)
        {
            _context.Entry(option).State = option.SectionProductId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(SectionProduct option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
