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

        public Address GetAddress(License license, AddressType addressType)
        {
            if (license == null)
            {
                return null;
            }

            if (addressType == null)
            {
                return null;
            }

            return license.Addresses.Where(a => a.AddressTypeId == addressType.AddressTypeId).FirstOrDefault();
        }
    }
}
