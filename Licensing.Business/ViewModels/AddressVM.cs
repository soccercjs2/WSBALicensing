using Licensing.Domain.Addresses;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AddressVM
    {
        public Address Address { get; set; }

        public AddressVM() { }

        public AddressVM(Address address)
        {
            Address = address;
        }
    }
}
