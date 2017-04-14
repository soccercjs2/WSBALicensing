using Licensing.Domain.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AddressStatesVM
    {
        public IList<AddressState> Codes { get; set; }
        public IList<AddressState> CodesToBeAdded { get; set; }
        public IList<AddressState> CodesToBeActivated { get; set; }
        public IList<AddressState> CodesToBeChanged { get; set; }
        public IList<AddressState> CodesToBeDeactivated { get; set; }
        public IList<AddressState> CodesToBeDeleted { get; set; }
    }
}
