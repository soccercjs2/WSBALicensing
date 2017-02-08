using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using Licensing.Domain.Licenses;

namespace Licensing.Data.Workers
{
    public class AddressWorker
    {
        private LicensingContext _context;

        public AddressWorker(LicensingContext context)
        {
            _context = context;
        }

        public Address GetAddress(int addressId)
        {
            return _context.Addresses.Find(addressId);
        }
    }
}
