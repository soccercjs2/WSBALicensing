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

        public AddressManager(LicensingContext context)
        {
            _context = context;
            _addressWorker = new AddressWorker(context);
        }

        public Address GetPrimaryAddress(License license)
        {
            //get primary address type
            AddressType primaryAddressType = _context.AddressTypes.Where(at => at.Name == "Primary").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(license, primaryAddressType);
        }

        public Address GetHomeAddress(License license)
        {
            //get home address type
            AddressType homeAddressType = _context.AddressTypes.Where(at => at.Name == "Home").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(license, homeAddressType);
        }

        public Address GetAgentOfServiceAddress(License license)
        {
            //get agent of service address
            AddressType agentOfServiceAddressType = _context.AddressTypes.Where(at => at.Name == "Agent Of Service").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(license, agentOfServiceAddressType);
        }
    }
}
