using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseProductPriceVM
    {
        public string AmsCode { get; set; }
        public LicenseProductPrice LicenseProductPrice { get; set; }
    }
}
