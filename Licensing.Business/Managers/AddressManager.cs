using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Addresses;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class AddressManager
    {
        private LicensingContext _context;
        private AddressWorker _addressWorker;

        public AddressManager(LicensingContext context, License license)
        {
            _context = context;
            _addressWorker = new AddressWorker(context, license);
        }

        public Address GetPrimaryAddress()
        {
            //get primary address type
            AddressType primaryAddressType = _context.AddressTypes.Where(at => at.Name == "Primary").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(primaryAddressType);
        }

        public Address GetHomeAddress()
        {
            //get home address type
            AddressType homeAddressType = _context.AddressTypes.Where(at => at.Name == "Home").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(homeAddressType);
        }

        public Address GetAgentOfServiceAddress()
        {
            //get agent of service address
            AddressType agentOfServiceAddressType = _context.AddressTypes.Where(at => at.Name == "Agent Of Service").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(agentOfServiceAddressType);
        }
    }
}
