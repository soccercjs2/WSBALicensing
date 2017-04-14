using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class LicenseTypeProductWorker
    {
        private LicensingContext _context;

        public LicenseTypeProductWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<LicenseProduct> GetProducts()
        {
            return _context.LicenseProducts.OrderBy(l => l.Name).ToList();
        }

        public void AddLicenseTypeProduct(LicenseTypeProduct licenseTypeProduct)
        {
            _context.Entry(licenseTypeProduct).State = EntityState.Added;
            _context.Entry(licenseTypeProduct.Product).State = EntityState.Unchanged;

            _context.SaveChanges();
        }

        public void DeleteLicenseTypeProduct(LicenseTypeProduct licenseTypeProduct)
        {
            _context.Entry(licenseTypeProduct).State = EntityState.Deleted;

            _context.SaveChanges();
        }
    }
}
