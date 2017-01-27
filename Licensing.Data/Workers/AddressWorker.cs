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
        private License _license;

        public AddressWorker(LicensingContext context, License license)
        {
            _context = context;
            _license = license;
        }

        public Address GetAddress(AddressType addressType)
        {
            if (_license.Addresses == null)
            {
                return null;
            }

            if (addressType == null)
            {
                return null;
            }

            return _license.Addresses.Where(a => a.AddressTypeId == addressType.AddressTypeId).FirstOrDefault();
        }
    }
}
