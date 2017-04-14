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
    public class LicenseTypeSectionWorker
    {
        private LicensingContext _context;

        public LicenseTypeSectionWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<SectionProduct> GetProducts()
        {
            return _context.SectionProducts.OrderBy(l => l.Name).ToList();
        }

        public void AddLicenseTypeSection(LicenseTypeSection licenseTypeSection)
        {
            _context.Entry(licenseTypeSection).State = EntityState.Added;
            _context.Entry(licenseTypeSection.Product).State = EntityState.Unchanged;

            _context.SaveChanges();
        }

        public void DeleteLicenseTypeSection(LicenseTypeSection licenseTypeSection)
        {
            _context.Entry(licenseTypeSection).State = EntityState.Deleted;

            _context.SaveChanges();
        }
    }
}
