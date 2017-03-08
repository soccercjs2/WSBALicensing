using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Donations
{
    public class DonationProduct : Activatable
    {
        public int DonationProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AmsCode { get; set; }
    }
}
