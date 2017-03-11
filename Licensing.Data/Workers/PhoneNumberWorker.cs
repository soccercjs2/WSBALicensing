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

        public void SetPhoneNumber(PhoneNumber phoneNumber)
        {
            _context.Entry(phoneNumber).State = phoneNumber.PhoneNumberId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.Entry(phoneNumber.PhoneNumberType).State = EntityState.Unchanged;

            _context.SaveChanges();
        }
    }
}
