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

        public AddressCountry GetAddressCountry(string code)
        {
            ICollection<AddressCountry> options = _context.AddressCountries.Where(c => c.AmsCode == code).ToList();

            foreach (AddressCountry option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<AddressCountry> GetAddressCountries()
        {
            return _context.AddressCountries.ToList();
        }

        public AddressState GetAddressState(string countryCode, string code)
        {
            ICollection<AddressState> options = _context.AddressStates.Where(c => c.AmsCode == code && c.AmsCountryCode == countryCode).ToList();

            foreach (AddressState option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<AddressState> GetAddressStates()
        {
            return _context.AddressStates.ToList();
        }

        public void SetAddress(Address address)
        {
            _context.Entry(address).State = address.AddressId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.Entry(address.AddressType).State = EntityState.Unchanged;

            _context.SaveChanges();
        }

        public ICollection<Address> GetResponsesWithAddressType(AddressType type)
        {
            return _context.Addresses.Where(f => f.AddressType.AddressTypeId == type.AddressTypeId).ToList();
        }

        public ICollection<Address> GetResponsesWithAddressCountry(AddressCountry country)
        {
            return _context.Addresses.Where(f => f.Country.AddressCountryId == country.AddressCountryId).ToList();
        }

        public ICollection<Address> GetResponsesWithAddressState(AddressState state)
        {
            return _context.Addresses.Where(f => f.State.AddressStateId == state.AddressStateId).ToList();
        }

        public void SetAddressType(AddressType type)
        {
            _context.Entry(type).State = type.AddressTypeId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteAddressType(AddressType type)
        {
            _context.Entry(type).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void SetAddressCountry(AddressCountry country)
        {
            _context.Entry(country).State = country.AddressCountryId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteAddressCountry(AddressCountry country)
        {
            _context.Entry(country).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void SetAddressState(AddressState state)
        {
            _context.Entry(state).State = state.AddressStateId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteAddressState(AddressState state)
        {
            _context.Entry(state).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
