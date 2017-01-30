using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Business.ViewModels
{
    public class PhoneNumbersVM
    {
        public string PrimaryPhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string FaxPhoneNumber { get; set; }

        public PhoneNumbersVM(PhoneNumber primaryPhoneNumber, PhoneNumber homePhoneNumber, PhoneNumber faxPhoneNumber)
        {
            PrimaryPhoneNumber = GetFormattedPhoneNumber(primaryPhoneNumber);
            HomePhoneNumber = GetFormattedPhoneNumber(homePhoneNumber);
            FaxPhoneNumber = GetFormattedPhoneNumber(faxPhoneNumber);
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