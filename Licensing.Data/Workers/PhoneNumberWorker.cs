using Licensing.Data.Context;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class PhoneNumberWorker
    {
        private LicensingContext _context;

        public PhoneNumberWorker(LicensingContext context)
        {
            _context = context;
        }

        public PhoneNumber GetPhoneNumber(int phoneNumberId)
        {
            return _context.PhoneNumbers.Find(phoneNumberId);
        }

        public PhoneNumberType GetPhoneNumberType(string addressType)
        {
            return _context.PhoneNumberTypes.Where(at => at.Name == addressType).FirstOrDefault();
        }

        public ICollection<PhoneNumberType> GetPhoneNumberTypes()
        {
            return _context.PhoneNumberTypes.ToList();
        }

        public void SetPhoneNumber(PhoneNumber phoneNumber)
        {
            _context.Entry(phoneNumber).State = phoneNumber.PhoneNumberId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.Entry(phoneNumber.PhoneNumberType).State = EntityState.Unchanged;

            _context.SaveChanges();
        }

        public PhoneNumberCountry GetCountry(string countryCode)
        {
            return _context.PhoneNumberCountries.Where(c => c.CountryCode == countryCode).FirstOrDefault();
        }

        public ICollection<PhoneNumberCountry> GetCountries()
        {
            return _context.PhoneNumberCountries.ToList();
        }

        public ICollection<PhoneNumber> GetResponsesWithCountry(PhoneNumberCountry country)
        {
            return _context.PhoneNumbers.Where(f => f.Country.PhoneNumberCountryId == country.PhoneNumberCountryId).ToList();
        }

        public void SetCountry(PhoneNumberCountry country)
        {
            _context.Entry(country).State = country.PhoneNumberCountryId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void SetType(PhoneNumberType type)
        {
            _context.Entry(type).State = type.PhoneNumberTypeId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteCountry(PhoneNumberCountry country)
        {
            _context.Entry(country).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
