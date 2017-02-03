using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Enums;
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

        public PhoneNumber GetPhoneNumber(int phoneNumberId)
        {
            return _phoneNumberWorker.GetPhoneNumber(phoneNumberId);
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

        public void Confirm(PhoneNumber phoneNumber)
        {
            _phoneNumberWorker.Confirm(phoneNumber);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            PhoneNumber primaryPhoneNumber = GetPrimaryPhoneNumber(license);
            PhoneNumber homePhoneNumber = GetHomePhoneNumber(license);

            return IsComplete(primaryPhoneNumber) && IsComplete(homePhoneNumber);
        }

        public bool IsComplete(PhoneNumber phoneNumber)
        {
            return (phoneNumber != null && phoneNumber.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            PhoneNumbersVM phoneNumbersVM = new PhoneNumbersVM(
                GetPrimaryPhoneNumber(license),
                GetHomePhoneNumber(license),
                GetFaxPhoneNumber(license)
            );

            return new DashboardContainerVM(
                "Phone Numbers",
                RequirementType.Optional,
                IsComplete(license),
                null,
                null,
                null,
                "_PhoneNumbers",
                phoneNumbersVM
            );
        }
    }
}
