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

        public AddressType GetAddressType(string code)
        {
            ICollection<AddressType> options = _context.AddressTypes.Where(c => c.AmsCode == code).ToList();

            foreach (AddressType option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<AddressType> GetAddressTypes()
        {
            return _context.AddressTypes.ToList();
        }

        public void SetAddress(Address address)
        {
            _context.Entry(address).State = address.AddressId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.Entry(address.AddressType).State = EntityState.Unchanged;

            _context.SaveChanges();
        }

        public ICollection<Address> GetResponsesWithOption(AddressType option)
        {
            return _context.Addresses.Where(f => f.AddressType.AddressTypeId == option.AddressTypeId).ToList();
        }

        public void SetOption(AddressType option)
        {
            _context.Entry(option).State = option.AddressTypeId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(AddressType option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
