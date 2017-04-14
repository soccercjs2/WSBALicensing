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

        public ICollection<PhoneNumberType> GetPhoneNumberTypes()
        {
            return _phoneNumberWorker.GetPhoneNumberTypes();
        }

        public PhoneNumberCountry GetCountry(string countryCode)
        {
            return _phoneNumberWorker.GetCountry(countryCode);
        }

        public ICollection<PhoneNumberCountry> GetCountries()
        {
            return _phoneNumberWorker.GetCountries();
        }

        public PhoneNumber GetPrimaryPhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType primaryPhoneNumberType = GetPhoneNumberType("Primary");

            if (license.PhoneNumbers == null) { return null; }

            //return phone number with primary phone number type
            return license.PhoneNumbers.Where(pn => pn.PhoneNumberType.PhoneNumberTypeId == primaryPhoneNumberType.PhoneNumberTypeId).FirstOrDefault();
        }

        public PhoneNumber GetHomePhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType homePhoneNumberType = GetPhoneNumberType("Home");

            if (license.PhoneNumbers == null) { return null; }

            //return phone number with home phone number type
            return license.PhoneNumbers.Where(pn => pn.PhoneNumberType.PhoneNumberTypeId == homePhoneNumberType.PhoneNumberTypeId).FirstOrDefault();
        }

        public PhoneNumber GetFaxPhoneNumber(License license)
        {
            //get primary phone number type
            PhoneNumberType faxPhoneNumberType = GetPhoneNumberType("Fax");

            if (license.PhoneNumbers == null) { return null; }

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

        public IList<PhoneNumberCountry> GetAmsPhoneNumberCountries()
        {
            IList<PhoneNumberCountry> options = new List<PhoneNumberCountry>();
            var codes = WSBA.AMS.CodeTypesManager.GetPhoneCountryList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new PhoneNumberCountry() { Name = code.Description, CountryCode = code.CountryCode, InternationalCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetCountry(PhoneNumberCountry option)
        {
            if (option.PhoneNumberCountryId == 0)
            {
                PhoneNumberCountry existingCode = _phoneNumberWorker.GetCountry(option.CountryCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    existingCode.InternationalCode = option.InternationalCode;
                    option = existingCode;
                }
            }

            _phoneNumberWorker.SetCountry(option);
        }

        public void DeleteCountry(PhoneNumberCountry option)
        {
            _phoneNumberWorker.DeleteCountry(option);
        }

        public IList<PhoneNumberCountry> GetCountriesToBeAdded(ICollection<PhoneNumberCountry> codes, ICollection<PhoneNumberCountry> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.CountryCode == ac.CountryCode)).ToList();
        }

        public IList<PhoneNumberCountry> GetCountriesToBeActivated(ICollection<PhoneNumberCountry> codes, ICollection<PhoneNumberCountry> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.CountryCode == ac.CountryCode)).ToList();
        }

        public IList<PhoneNumberCountry> GetCountriesToBeChanged(ICollection<PhoneNumberCountry> codes, ICollection<PhoneNumberCountry> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.CountryCode == ac.CountryCode && (c.Name != ac.Name || c.InternationalCode != ac.InternationalCode))).ToList();
        }

        public IList<PhoneNumberCountry> GetCountriesToBeDeactivated(ICollection<PhoneNumberCountry> codes, ICollection<PhoneNumberCountry> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<PhoneNumberCountry> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.CountryCode == c.CountryCode)).ToList();
            IList<PhoneNumberCountry> codesToDeactivate = new List<PhoneNumberCountry>();

            foreach (PhoneNumberCountry option in codesToRemove)
            {
                ICollection<PhoneNumber> responsesWithOption = _phoneNumberWorker.GetResponsesWithCountry(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<PhoneNumberCountry> GetCountriesToBeDeleted(ICollection<PhoneNumberCountry> codes, ICollection<PhoneNumberCountry> amsCodes)
        {
            IList<PhoneNumberCountry> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.CountryCode == c.CountryCode)).ToList();
            IList<PhoneNumberCountry> codesToDeleted = new List<PhoneNumberCountry>();

            foreach (PhoneNumberCountry option in codesToRemove)
            {
                ICollection<PhoneNumber> responsesWithOption = _phoneNumberWorker.GetResponsesWithCountry(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public void SetType(PhoneNumberType type)
        {
            if (type.PhoneNumberTypeId == 0)
            {
                PhoneNumberType existingType = _phoneNumberWorker.GetPhoneNumberType(type.Name);

                if (existingType != null)
                {
                    existingType.Active = true;
                    existingType.Name = type.Name;
                    type = existingType;
                }
            }

            _phoneNumberWorker.SetType(type);
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

            if (license.LicenseType.LicenseTypeRequirement.PrimaryPhoneNumber == RequirementType.Required || 
                license.LicenseType.LicenseTypeRequirement.HomePhoneNumber == RequirementType.Required || 
                license.LicenseType.LicenseTypeRequirement.FaxPhoneNumber == RequirementType.Required)
            {
                requirementType = RequirementType.Required;
            }

            if (license.LicenseType.LicenseTypeRequirement.PrimaryPhoneNumber == RequirementType.Excluded &&
                license.LicenseType.LicenseTypeRequirement.HomePhoneNumber == RequirementType.Excluded &&
                license.LicenseType.LicenseTypeRequirement.FaxPhoneNumber == RequirementType.Excluded)
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
