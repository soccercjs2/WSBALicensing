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
    public class LicenseTypeWorker
    {
        private LicensingContext _context;

        public LicenseTypeWorker(LicensingContext context)
        {
            _context = context;
        }

        public LicenseType GetLicenseType(string type)
        {
            return _context.LicenseTypes.Where(lt => lt.Name == type).FirstOrDefault();
        }

        public LicenseType GetLicenseType(int id)
        {
            return _context.LicenseTypes.Find(id);
        }

        public ICollection<LicenseType> GetLicenseTypes()
        {
            return _context.LicenseTypes.OrderBy(l => l.Name).ToList();
        }

        public void SetLicenseType(LicenseType licenseType)
        {
            _context.Entry(licenseType).State = licenseType.LicenseTypeId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
