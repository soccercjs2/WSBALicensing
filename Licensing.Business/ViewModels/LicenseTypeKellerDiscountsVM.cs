using Licensing.Domain.Enums;
using Licensing.Domain.Keller;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseTypeKellerDiscountsVM
    {
        public int LicenseTypeId { get; set; }
        public string Name { get; set; }
        public LicenseTypeKellerDiscountVM KellerDiscountVM { get; set; }
        public IList<LicenseTypeKellerDiscountVM> ExcludedKellerDiscounts { get; set; }

        public LicenseTypeKellerDiscountsVM() { }

        public LicenseTypeKellerDiscountsVM(LicenseType licenseType, IList<LicenseTypeKellerDiscountVM> excludedKellerDiscounts)
        {
            LicenseTypeId = licenseType.LicenseTypeId;
            Name = licenseType.Name;
            ExcludedKellerDiscounts = excludedKellerDiscounts;

            if (licenseType.KellerDiscount != null)
            {
                KellerDiscountVM = new LicenseTypeKellerDiscountVM(licenseType.KellerDiscount);
            }
        }
    }
}
