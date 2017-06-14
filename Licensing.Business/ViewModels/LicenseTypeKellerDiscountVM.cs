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
    public class LicenseTypeKellerDiscountVM
    {
        public KellerDiscount KellerDiscount { get; set; }
        public bool Flag { get; set; }

        public LicenseTypeKellerDiscountVM() { }

        public LicenseTypeKellerDiscountVM(KellerDiscount kellerDiscount)
        {
            KellerDiscount = kellerDiscount;
            Flag = false;
        }
    }
}
