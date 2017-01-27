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

        public PhoneNumberManager(LicensingContext context, License license)
        {
            _context = context;
            _phoneNumberWorker = new PhoneNumberWorker(context, license);
        }

        public PhoneNumber GetPrimaryPhoneNumber()
        {
            //get primary phone number type
            PhoneNumberType primaryPhoneNumberType = _context.PhoneNumberTypes.Where(pt => pt.Name == "Primary").FirstOrDefault();

            //retrun phone number
            return _phoneNumberWorker.GetPhoneNumber(primaryPhoneNumberType);
        }

        public PhoneNumber GetHomePhoneNumber()
        {
            //get primary phone number type
            PhoneNumberType homePhoneNumberType = _context.PhoneNumberTypes.Where(pt => pt.Name == "Home").FirstOrDefault();

            //retrun phone number
            return _phoneNumberWorker.GetPhoneNumber(homePhoneNumberType);
        }

        public PhoneNumber GetFaxPhoneNumber()
        {
            //get primary phone number type
            PhoneNumberType faxPhoneNumberType = _context.PhoneNumberTypes.Where(pt => pt.Name == "Fax").FirstOrDefault();

            //retrun phone number
            return _phoneNumberWorker.GetPhoneNumber(faxPhoneNumberType);
        }

        public string GetFormattedPhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumber == null)
            {
                return null;
            }

            string formattedPhoneNumber = "";
            string formattedInternationalPhoneNumber = "";

            //formatting country code
            formattedInternationalPhoneNumber += "+" + phoneNumber.CountryCode;

            //formatting area code
            formattedInternationalPhoneNumber += phoneNumber.AreaCode;
            formattedPhoneNumber += "(" + phoneNumber.AreaCode + ") ";

            //formatting phone number
            formattedInternationalPhoneNumber += phoneNumber.ExchangeCode + phoneNumber.LineNumber;
            formattedPhoneNumber += phoneNumber.ExchangeCode + "-" + phoneNumber.LineNumber;

            if (phoneNumber.CountryCode == 1)
            {
                return formattedPhoneNumber;
            }
            else
            {
                return formattedInternationalPhoneNumber;
            }
        }
    }
}
