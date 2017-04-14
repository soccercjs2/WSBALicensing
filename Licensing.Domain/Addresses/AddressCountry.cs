using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Addresses
{
    public class AddressCountry : Activatable
    {
        public int AddressCountryId { get; set; }
        public string Name { get; set; }
        public string AmsCode { get; set; }
    }
}
