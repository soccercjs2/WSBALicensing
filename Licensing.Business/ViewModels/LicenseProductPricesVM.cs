using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LicenseProductPricesVM
    {
        public IList<LicenseProductPriceVM> Prices { get; set; }
        public IList<LicenseProductPriceVM> PricesToBeAdded { get; set; }
        public IList<LicenseProductPriceVM> PricesToBeChanged { get; set; }
        public IList<LicenseProductPriceVM> PricesToBeDeleted { get; set; }
    }
}
