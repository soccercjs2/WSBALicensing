using Licensing.Domain.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AddressTypesVM
    {
        public IList<AddressType> Codes { get; set; }
        public IList<AddressType> CodesToBeAdded { get; set; }
        public IList<AddressType> CodesToBeActivated { get; set; }
        public IList<AddressType> CodesToBeChanged { get; set; }
        public IList<AddressType> CodesToBeDeactivated { get; set; }
        public IList<AddressType> CodesToBeDeleted { get; set; }
    }
}
