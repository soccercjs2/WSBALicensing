using Licensing.Data.Context;
using Licensing.Domain.Donations;
using Licensing.Domain.Keller;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class LicenseTypeKellerDiscountWorker
    {
        private LicensingContext _context;

        public LicenseTypeKellerDiscountWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<KellerDiscount> GetKellerDiscounts()
        {
            return _context.KellerDiscounts.OrderBy(l => l.Name).ToList();
        }

        public void SetKellerDiscount(KellerDiscount kellerDiscount)
        {
            _context.Entry(kellerDiscount).State = kellerDiscount.KellerDiscountId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteKellerDiscount(KellerDiscount kellerDiscount)
        {
            _context.Entry(kellerDiscount).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
