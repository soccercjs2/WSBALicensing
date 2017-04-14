using Licensing.Data.Context;
using Licensing.Domain.Donations;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class LicenseTypeDonationWorker
    {
        private LicensingContext _context;

        public LicenseTypeDonationWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<DonationProduct> GetProducts()
        {
            return _context.DonationProducts.OrderBy(l => l.Name).ToList();
        }

        public void AddLicenseTypeDonation(LicenseTypeDonation licenseTypeDonation)
        {
            _context.Entry(licenseTypeDonation).State = EntityState.Added;
            _context.Entry(licenseTypeDonation.Product).State = EntityState.Unchanged;

            _context.SaveChanges();
        }

        public void DeleteLicenseTypeDonation(LicenseTypeDonation licenseTypeDonation)
        {
            _context.Entry(licenseTypeDonation).State = EntityState.Deleted;

            _context.SaveChanges();
        }
    }
}
