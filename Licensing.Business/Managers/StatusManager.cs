using Licensing.Business.Enums;
using Licensing.Data.Context;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class StatusManager
    {
        private LicensingContext _context;

        public StatusManager(LicensingContext context)
        {
            _context = context;
        }

        public LicensingStatus GetLicensingStatus(License license)
        {
            JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
            ProBonoManager proBonoManager = new ProBonoManager(_context);
            AddressManager addressManager = new AddressManager(_context);
            EmailManager emailManager = new EmailManager(_context);
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);
            FirmSizeManager firmSizeManager = new FirmSizeManager(_context);
            LanguageManager languageManager = new LanguageManager(_context);
            EthnicityManager ethnicityManager = new EthnicityManager(_context);
            GenderManager genderManager = new GenderManager(_context);
            DisabilityManager disabilityManager = new DisabilityManager(_context);
            SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);
            MembershipProductManager membershipProductManager = new MembershipProductManager(_context);
            SectionManager sectionManager = new SectionManager(_context);
            DonationManager donationManager = new DonationManager(_context);
            BarNewsManager barNewsManager = new BarNewsManager(_context);

            LicensingStatus licensingStatus = LicensingStatus.Complete;

            if (license.LicenseType.JudicialPosition == RequirementType.Required && !judicialPositionManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.TrustAccount == RequirementType.Required && !trustAccountManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.ProfessionalLiabilityInsurance == RequirementType.Required && !professionalLiabilityInsuranceManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.FinancialResponsibility == RequirementType.Required && !financialResponsibilityManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.ProBono == RequirementType.Required && !proBonoManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            if (license.LicenseType.PrimaryAddress == RequirementType.Required && !addressManager.IsComplete(addressManager.GetPrimaryAddress(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.HomeAddress == RequirementType.Required && !addressManager.IsComplete(addressManager.GetHomeAddress(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.AgentOfServiceAddress == RequirementType.Required && !addressManager.IsComplete(addressManager.GetAgentOfServiceAddress(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.PrimaryEmail == RequirementType.Required && !emailManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.PrimaryPhoneNumber == RequirementType.Required && !phoneNumberManager.IsComplete(phoneNumberManager.GetPrimaryPhoneNumber(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.HomePhoneNumber == RequirementType.Required && !phoneNumberManager.IsComplete(phoneNumberManager.GetHomePhoneNumber(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.FaxPhoneNumber == RequirementType.Required && addressManager.AgentOfServiceAddressRequired(license) && !phoneNumberManager.IsComplete(phoneNumberManager.GetFaxPhoneNumber(license))) { licensingStatus = LicensingStatus.Incomplete; }

            if (license.LicenseType.AreasOfPractice == RequirementType.Required && !areaOfPracticeManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.FirmSize == RequirementType.Required && !firmSizeManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.Languages == RequirementType.Required && !languageManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            if (license.LicenseType.Ethnicity == RequirementType.Required && !ethnicityManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.Gender == RequirementType.Required && !genderManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.Disability == RequirementType.Required && !disabilityManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.SexualOrientation == RequirementType.Required && !sexualOrientationManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            if (license.LicenseType.Sections == RequirementType.Required && !sectionManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.Donations == RequirementType.Required && !donationManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.BarNews == RequirementType.Required && !barNewsManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            return licensingStatus;
        }
    }
}
