using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class PhoneNumberManager
    {
        private LicensingContext _context;
        private PhoneNumberWorker _phoneNumberWorker;

        public PhoneNumberManager(LicensingContext context)
        {
            _context = context;
            _phoneNumberWorker = new PhoneNumberWorker(context);
        }

        public PhoneNumber GetPrimaryPhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType primaryPhoneNumberType = _context.PhoneNumberTypes.Where(pt => pt.Name == "Primary").FirstOrDefault();

            //retrun phone number
            return _phoneNumberWorker.GetPhoneNumber(license, primaryPhoneNumberType);
        }

        public PhoneNumber GetHomePhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType homePhoneNumberType = _context.PhoneNumberTypes.Where(pt => pt.Name == "Home").FirstOrDefault();

            //retrun phone number
            return _phoneNumberWorker.GetPhoneNumber(license, homePhoneNumberType);
        }

        public PhoneNumber GetFaxPhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType faxPhoneNumberType = _context.PhoneNumberTypes.Where(pt => pt.Name == "Fax").FirstOrDefault();

            //retrun phone number
            return _phoneNumberWorker.GetPhoneNumber(license, faxPhoneNumberType);
        }
    }
}
