using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using Licensing.Domain.Licenses;
using System.Data.Entity;

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

        public AddressType GetAddressType(string addressType)
        {
            return _context.AddressTypes.Where(at => at.Name == addressType).FirstOrDefault();
        }

        public void SetAddress(Address address)
        {
            _context.Entry(address).State = address.AddressId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.Entry(address.AddressType).State = EntityState.Unchanged;

            _context.SaveChanges();
        }
    }
}
