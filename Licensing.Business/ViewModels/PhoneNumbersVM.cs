using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Business.ViewModels
{
    public class PhoneNumbersVM
    {
        public PhoneNumber PrimaryPhoneNumber { get; set; }
        public PhoneNumber HomePhoneNumber { get; set; }
        public PhoneNumber FaxPhoneNumber { get; set; }

        public PhoneNumbersVM(PhoneNumber primaryPhoneNumber, PhoneNumber homePhoneNumber, PhoneNumber faxPhoneNumber)
        {
            PrimaryPhoneNumber = primaryPhoneNumber;
            HomePhoneNumber = homePhoneNumber;
            FaxPhoneNumber = faxPhoneNumber;
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