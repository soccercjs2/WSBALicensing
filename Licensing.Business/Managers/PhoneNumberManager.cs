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

        public PhoneNumberType GetPhoneNumberType(string addressType)
        {
            return _phoneNumberWorker.GetPhoneNumberType(addressType);
        }

        public PhoneNumber GetPrimaryPhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType primaryPhoneNumberType = GetPhoneNumberType("Primary");

            //return phone number with primary phone number type
            return license.PhoneNumbers.Where(pn => pn.PhoneNumberType.PhoneNumberTypeId == primaryPhoneNumberType.PhoneNumberTypeId).FirstOrDefault();
        }

        public PhoneNumber GetHomePhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType homePhoneNumberType = GetPhoneNumberType("Home");

            //return phone number with home phone number type
            return license.PhoneNumbers.Where(pn => pn.PhoneNumberType.PhoneNumberTypeId == homePhoneNumberType.PhoneNumberTypeId).FirstOrDefault();
        }

        public PhoneNumber GetFaxPhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType faxPhoneNumberType = GetPhoneNumberType("Fax");

            //return phone number with fax phone number type
            return license.PhoneNumbers.Where(pn => pn.PhoneNumberType.PhoneNumberTypeId == faxPhoneNumberType.PhoneNumberTypeId).FirstOrDefault();
        }

        public void SetPhoneNumber(PhoneNumber phoneNumber)
        {
            phoneNumber.Confirmed = true;
            _phoneNumberWorker.SetPhoneNumber(phoneNumber);
        }

        public void Confirm(PhoneNumber phoneNumber)
        {
            phoneNumber.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
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
                license,
                GetPrimaryPhoneNumber(license),
                GetHomePhoneNumber(license),
                GetFaxPhoneNumber(license)
            );

            RequirementType requirementType = RequirementType.Optional;

            if (license.LicenseType.PrimaryPhoneNumber == RequirementType.Required || 
                license.LicenseType.HomePhoneNumber == RequirementType.Required || 
                license.LicenseType.FaxPhoneNumber == RequirementType.Required)
            {
                requirementType = RequirementType.Required;
            }

            if (license.LicenseType.PrimaryPhoneNumber == RequirementType.Excluded &&
                license.LicenseType.HomePhoneNumber == RequirementType.Excluded &&
                license.LicenseType.FaxPhoneNumber == RequirementType.Excluded)
            {
                requirementType = RequirementType.Excluded;
            }

            return new DashboardContainerVM(
                "Phone Numbers",
                requirementType,
                IsComplete(license),
                null,
                null,
                false,
                "_PhoneNumbers",
                phoneNumbersVM
            );
        }
    }
}
