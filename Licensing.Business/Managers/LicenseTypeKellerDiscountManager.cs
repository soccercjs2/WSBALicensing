using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Donations;
using Licensing.Domain.Keller;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class LicenseTypeKellerDiscountManager
    {
        private LicensingContext _context;
        private LicenseTypeKellerDiscountWorker _licenseTypeKellerDiscountWorker;

        public LicenseTypeKellerDiscountManager(LicensingContext context)
        {
            _context = context;
            _licenseTypeKellerDiscountWorker = new LicenseTypeKellerDiscountWorker(context);
        }

        public ICollection<LicenseTypeKellerDiscountVM> GetExcludedKellerDiscounts(LicenseType licenseType)
        {
            ICollection<KellerDiscount> kellerDiscounts = _licenseTypeKellerDiscountWorker.GetKellerDiscounts();

            if (licenseType.KellerDiscount != null)
            {
                kellerDiscounts.Remove(licenseType.KellerDiscount);
            }

            return kellerDiscounts.Select(kd => new LicenseTypeKellerDiscountVM(kd)).ToList();
        }

        public void SetLicenseTypeKellerDiscount(LicenseType licenseType, KellerDiscount kellerDiscount)
        {
            licenseType.KellerDiscountId = kellerDiscount.KellerDiscountId;
            _context.SaveChanges();
        }

        public void DeleteLicenseTypeKellerDiscount(LicenseType licenseType)
        {
            licenseType.KellerDiscountId = null;
            _context.SaveChanges();
        }
    }
}
