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
        public ICollection<AddressCountry> Countries { get; set; }
        public ICollection<AddressState> States { get; set; }
        public int AddressCountryIdStatesLoadedFor { get; set; }

        public AddressVM() { }

        public AddressVM(Address address, ICollection<AddressCountry> countries, ICollection<AddressState> states, int addressCountryIdStatesLoadedFor)
        {
            Address = address;
            Countries = countries;
            States = states;
            AddressCountryIdStatesLoadedFor = addressCountryIdStatesLoadedFor;
        }
    }
}
