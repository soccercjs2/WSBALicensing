using Licensing.Domain.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AddressCountriesVM
    {
        public IList<AddressCountry> Codes { get; set; }
        public IList<AddressCountry> CodesToBeAdded { get; set; }
        public IList<AddressCountry> CodesToBeActivated { get; set; }
        public IList<AddressCountry> CodesToBeChanged { get; set; }
        public IList<AddressCountry> CodesToBeDeactivated { get; set; }
        public IList<AddressCountry> CodesToBeDeleted { get; set; }
    }
}
