using Licensing.Data.Context;
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
    public class KellerDiscountWorker
    {
        private LicensingContext _context;

        public KellerDiscountWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<KellerDiscount> GetKellerDiscounts()
        {
            return _context.KellerDiscounts.OrderBy(o => o.Name).ToList();
        }

        public KellerDiscount GetKellerDiscount(string productDiscountId)
        {
            ICollection<KellerDiscount> discounts = _context.KellerDiscounts.Where(c => c.AmsProductDiscountId == productDiscountId).ToList();

            foreach (KellerDiscount discount in discounts)
            {
                if (discount.AmsProductDiscountId == productDiscountId)
                {
                    return discount;
                }
            }

            return null;
        }

        public ICollection<LicenseType> GetLicenseTypesWithDiscount(KellerDiscount discount)
        {
            return _context.LicenseTypes.Where(f => f.KellerDiscount.AmsProductDiscountId == discount.AmsProductDiscountId).ToList();
        }

        public void SetKellerDiscount(KellerDiscount discount)
        {
            _context.Entry(discount).State = discount.KellerDiscountId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteKellerDiscount(KellerDiscount discount)
        {
            _context.Entry(discount).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
