using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Keller;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class KellerDiscountManager
    {
        private LicensingContext _context;
        private KellerDiscountWorker _kellerDiscountWorker;

        public KellerDiscountManager(LicensingContext context)
        {
            _context = context;
            _kellerDiscountWorker = new KellerDiscountWorker(context);
        }

        public ICollection<KellerDiscount> GetKellerDiscounts()
        {
            return _kellerDiscountWorker.GetKellerDiscounts();
        }

        public KellerDiscount GetKellerDiscount(string productDiscountId)
        {
            return _kellerDiscountWorker.GetKellerDiscount(productDiscountId);
        }

        public IList<KellerDiscount> GetAmsDiscounts()
        {
            IList<KellerDiscount> discounts = new List<KellerDiscount>();
            var amsDiscounts = WSBA.AMS.ProductManager.GetKellerDiscounts().OrderBy(c => c.Description);

            foreach (var amsDiscount in amsDiscounts)
            {
                discounts.Add(new KellerDiscount()
                {
                    AmsProductDiscountId = amsDiscount.ProductDiscountID,
                    Name = amsDiscount.Description,
                    DiscountPercentage = amsDiscount.DiscountPercentage,
                    Active = true
                });
            }

            return discounts;
        }

        public IList<KellerDiscount> GetDiscountsToBeAdded(ICollection<KellerDiscount> discounts, ICollection<KellerDiscount> amsProductDiscounts)
        {
            return amsProductDiscounts.Where(ac => !discounts.Any(c => c.AmsProductDiscountId == ac.AmsProductDiscountId)).ToList();
        }

        public IList<KellerDiscount> GetDiscountsToBeActivated(ICollection<KellerDiscount> discounts, ICollection<KellerDiscount> amsProductDiscounts)
        {
            //get inactive discounts
            discounts = discounts.Where(c => !c.Active).ToList();
            return discounts.Where(c => amsProductDiscounts.Any(ac => c.AmsProductDiscountId == ac.AmsProductDiscountId)).ToList();
        }

        public IList<KellerDiscount> GetDiscountsToBeChanged(ICollection<KellerDiscount> discounts, ICollection<KellerDiscount> amsProductDiscounts)
        {
            return amsProductDiscounts.Where(ac => discounts.Any(c => c.AmsProductDiscountId == ac.AmsProductDiscountId && c.Name != ac.Name)).ToList();
        }

        public IList<KellerDiscount> GetDiscountsToBeDeactivated(ICollection<KellerDiscount> discounts, ICollection<KellerDiscount> amsProductDiscounts)
        {
            //get active codes
            discounts = discounts.Where(c => c.Active).ToList();

            IList<KellerDiscount> discountsToRemove = discounts.Where(c => !amsProductDiscounts.Any(ac => ac.AmsProductDiscountId == c.AmsProductDiscountId)).ToList();
            IList<KellerDiscount> discountsToDeactivate = new List<KellerDiscount>();

            foreach (KellerDiscount discountToRemove in discountsToRemove)
            {
                ICollection<LicenseType> licenseTypesWithDonation = _kellerDiscountWorker.GetLicenseTypesWithDiscount(discountToRemove);
                if (licenseTypesWithDonation != null && licenseTypesWithDonation.Count > 0)
                {
                    discountsToDeactivate.Add(discountToRemove);
                }
            }

            return discountsToDeactivate;
        }

        public IList<KellerDiscount> GetDiscountsToBeDeleted(ICollection<KellerDiscount> codes, ICollection<KellerDiscount> amsCodes)
        {
            IList<KellerDiscount> discountsToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsProductDiscountId == c.AmsProductDiscountId)).ToList();
            IList<KellerDiscount> discountsToDeactivate = new List<KellerDiscount>();

            foreach (KellerDiscount discountToRemove in discountsToRemove)
            {
                ICollection<LicenseType> licenseTypesWithDonation = _kellerDiscountWorker.GetLicenseTypesWithDiscount(discountToRemove);
                if (licenseTypesWithDonation == null || licenseTypesWithDonation.Count == 0)
                {
                    discountsToDeactivate.Add(discountToRemove);
                }
            }

            return discountsToDeactivate;
        }

        public void SetKellerDiscount(KellerDiscount discount)
        {
            if (discount.KellerDiscountId == 0)
            {
                KellerDiscount existingDiscount = _kellerDiscountWorker.GetKellerDiscount(discount.AmsProductDiscountId);

                if (existingDiscount != null)
                {
                    existingDiscount.Active = true;
                    existingDiscount.Name = discount.Name;
                    existingDiscount.DiscountPercentage = discount.DiscountPercentage;
                    discount = existingDiscount;
                }
            }

            _kellerDiscountWorker.SetKellerDiscount(discount);
        }

        public void SetKellerDiscount(License license, bool takeKellerDiscount)
        {
            license.KellerDeduction = takeKellerDiscount;
            _context.SaveChanges();
        }
    }
}
