using Licensing.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class DonationProductsVM
    {
        public IList<DonationProduct> Codes { get; set; }
        public IList<DonationProduct> CodesToBeAdded { get; set; }
        public IList<DonationProduct> CodesToBeActivated { get; set; }
        public IList<DonationProduct> CodesToBeChanged { get; set; }
        public IList<DonationProduct> CodesToBeDeactivated { get; set; }
        public IList<DonationProduct> CodesToBeDeleted { get; set; }
    }
}
