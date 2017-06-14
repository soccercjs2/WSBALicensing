using Licensing.Domain.Keller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class KellerDiscountsVM
    {
        public IList<KellerDiscount> Discounts { get; set; }
        public IList<KellerDiscount> DiscountsToBeAdded { get; set; }
        public IList<KellerDiscount> DiscountsToBeActivated { get; set; }
        public IList<KellerDiscount> DiscountsToBeChanged { get; set; }
        public IList<KellerDiscount> DiscountsToBeDeactivated { get; set; }
        public IList<KellerDiscount> DiscountsToBeDeleted { get; set; }
    }
}
