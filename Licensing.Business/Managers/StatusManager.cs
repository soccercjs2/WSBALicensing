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
            MCLEManager mcleManager = new MCLEManager(_context);
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

            if (license.LicenseType.LicenseTypeRequirement.JudicialPosition == RequirementType.Required && !judicialPositionManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.TrustAccount == RequirementType.Required && !trustAccountManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.ProfessionalLiabilityInsurance == RequirementType.Required && !professionalLiabilityInsuranceManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.FinancialResponsibility == RequirementType.Required && !financialResponsibilityManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.ProBono == RequirementType.Required && !proBonoManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.MCLE == RequirementType.Required && !mcleManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            if (license.LicenseType.LicenseTypeRequirement.PrimaryAddress == RequirementType.Required && !addressManager.IsComplete(addressManager.GetPrimaryAddress(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.HomeAddress == RequirementType.Required && !addressManager.IsComplete(addressManager.GetHomeAddress(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.AgentOfServiceAddress == RequirementType.Required && !addressManager.IsComplete(addressManager.GetAgentOfServiceAddress(license)) && addressManager.AgentOfServiceAddressRequired(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.PrimaryEmail == RequirementType.Required && !emailManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.PrimaryPhoneNumber == RequirementType.Required && !phoneNumberManager.IsComplete(phoneNumberManager.GetPrimaryPhoneNumber(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.HomePhoneNumber == RequirementType.Required && !phoneNumberManager.IsComplete(phoneNumberManager.GetHomePhoneNumber(license))) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.FaxPhoneNumber == RequirementType.Required && addressManager.AgentOfServiceAddressRequired(license) && !phoneNumberManager.IsComplete(phoneNumberManager.GetFaxPhoneNumber(license))) { licensingStatus = LicensingStatus.Incomplete; }

            if (license.LicenseType.LicenseTypeRequirement.AreasOfPractice == RequirementType.Required && !areaOfPracticeManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.FirmSize == RequirementType.Required && !firmSizeManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.Languages == RequirementType.Required && !languageManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            if (license.LicenseType.LicenseTypeRequirement.Ethnicity == RequirementType.Required && !ethnicityManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.Gender == RequirementType.Required && !genderManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.Disability == RequirementType.Required && !disabilityManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.SexualOrientation == RequirementType.Required && !sexualOrientationManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            if (!membershipProductManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.Sections == RequirementType.Required && !sectionManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.Donations == RequirementType.Required && !donationManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }
            if (license.LicenseType.LicenseTypeRequirement.BarNews == RequirementType.Required && !barNewsManager.IsComplete(license)) { licensingStatus = LicensingStatus.Incomplete; }

            return licensingStatus;
        }
    }
}
