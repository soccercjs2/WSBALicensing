using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Donations
{
    public class DonationProduct
    {
        public int DonationProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string AmsCode { get; set; }
    }
}
