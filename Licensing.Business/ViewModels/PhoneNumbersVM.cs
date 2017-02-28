using Licensing.Domain.ContactInformation;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Business.ViewModels
{
    public class PhoneNumbersVM
    {
        public int LicenseId { get; set; }
        public PhoneNumber PrimaryPhoneNumber { get; set; }
        public PhoneNumber HomePhoneNumber { get; set; }
        public PhoneNumber FaxPhoneNumber { get; set; }
        public RequirementType PrimaryPhoneNumberRequirementType { get; set; }
        public RequirementType HomePhoneNumberRequirementType { get; set; }
        public RequirementType FaxPhoneNumberRequirementType { get; set; }

        public PhoneNumbersVM(License license, PhoneNumber primaryPhoneNumber, PhoneNumber homePhoneNumber, PhoneNumber faxPhoneNumber)
        {
            LicenseId = license.LicenseId;
            PrimaryPhoneNumber = primaryPhoneNumber;
            HomePhoneNumber = homePhoneNumber;
            FaxPhoneNumber = faxPhoneNumber;
            PrimaryPhoneNumberRequirementType = license.LicenseType.PrimaryPhoneNumber;
            HomePhoneNumberRequirementType = license.LicenseType.HomePhoneNumber;
            FaxPhoneNumberRequirementType = license.LicenseType.FaxPhoneNumber;
        }
    }
}