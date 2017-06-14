using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Enums;
using Licensing.Domain.Languages;
using Licensing.Domain.Licenses;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class AmsWriterManager
    {
        private LicensingContext _context;

        public AmsWriterManager(LicensingContext context)
        {
            _context = context;
        }

        public void Save(License license)
        {
            //create master customer id
            string masterCustomerId = "000000000000".Substring(0, 12 - license.Customer.BarNumber.Length) + license.Customer.BarNumber;

            //WriteMemberType(license, masterCustomerId);
            WriteFinancialResponsibility(license, masterCustomerId);
            WriteJudicialPosition(license, masterCustomerId);
            WritePracticeAreas(license, masterCustomerId);
            WriteProBono(license, masterCustomerId);
            WriteProfessionalLiabilityInsurance(license, masterCustomerId);
            WriteTrustAccount(license, masterCustomerId);
            WritePrimaryAddress(license, masterCustomerId);
            WriteHomeAddress(license, masterCustomerId);
            WriteAgentOfServiceAddress(license, masterCustomerId);
            WriteEmail(license, masterCustomerId);
            WritePrimaryPhone(license, masterCustomerId);
            WriteHomePhone(license, masterCustomerId);
            WriteFaxPhone(license, masterCustomerId);
            WriteGender(license, masterCustomerId);
            WriteEthnicity(license, masterCustomerId);
            WriteSexualOrientation(license, masterCustomerId);
            WriteDisability(license, masterCustomerId);
            WriteAreasOfPractice(license, masterCustomerId);
            WriteFirmSize(license, masterCustomerId);
            WriteLanguages(license, masterCustomerId);
        }

        private void WriteMemberType(License license, string masterCustomerId)
        {
            if (license.LicenseType != null && license.LicenseType.LicenseTypeRequirement.MembershipType != RequirementType.Excluded)
            {
                WSBA.AMS.MemberManager.SetMemberStatus(masterCustomerId, license.LicenseType.AmsMemberType);
            }
        }

        private void WriteFinancialResponsibility(License license, string masterCustomerId)
        {
            if (license.FinancialResponsibility != null && license.LicenseType.LicenseTypeRequirement.FinancialResponsibility != RequirementType.Excluded)
            {
                int amsFinancialResponsibilityId = WSBA.AMS.MemberManager.SetFinancialResponsibility(masterCustomerId, license.LicensePeriod.EndDate.Year,
                        license.FinancialResponsibility.Option.AmsCode, license.FinancialResponsibility.Company, license.FinancialResponsibility.PolicyNumber);

                FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
                financialResponsibilityManager.SetFinancialResponsibility(license, 
                    license.FinancialResponsibility.Company, license.FinancialResponsibility.PolicyNumber, license.FinancialResponsibility.Option.CoveredByOptionId);
            }
        }

        private void WriteJudicialPosition(License license, string masterCustomerId)
        {
            if (license.JudicialPosition != null && license.LicenseType.LicenseTypeRequirement.JudicialPosition != RequirementType.Excluded)
            {
                WSBA.AMS.DemographicManager.SetActiveJudicialPosition(masterCustomerId, license.JudicialPosition.Option.AmsCode);
            }
        }

        private void WritePracticeAreas(License license, string masterCustomerId)
        {
            if (license.PracticeAreas != null && license.LicenseType.LicenseTypeRequirement.PracticeAreas != RequirementType.Excluded)
            {
                List<string> practiceAreaCodes = new List<string>();

                foreach (var practiceArea in license.PracticeAreas) { practiceAreaCodes.Add(practiceArea.Option.AmsCode); }

                WSBA.AMS.MemberManager.SetActivePracticeAreas(masterCustomerId, practiceAreaCodes);
            }
        }

        private void WriteProBono(License license, string masterCustomerId)
        {
            if (license.ProBono != null && license.LicenseType.LicenseTypeRequirement.ProBono != RequirementType.Excluded && license.ProBono.ProvidesService)
            {
                WSBA.AMS.MemberManager.SetProBono(masterCustomerId, license.LicensePeriod.EndDate.Year, 
                    license.ProBono.FreeServiceHours, license.ProBono.LimitedFeeServiceHours, license.ProBono.Anonymous);
            }
        }

        private void WriteProfessionalLiabilityInsurance(License license, string masterCustomerId)
        {
            if (license.ProfessionalLiabilityInsurance != null && license.LicenseType.LicenseTypeRequirement.ProfessionalLiabilityInsurance != RequirementType.Excluded)
            {
                WSBA.AMS.MemberManager.SetInsuranceDisclosure(masterCustomerId, license.LicensePeriod.EndDate.Year,
                    license.ProfessionalLiabilityInsurance.Option.PrivatePractice, 
                    license.ProfessionalLiabilityInsurance.Option.CurrentlyInsured, 
                    license.ProfessionalLiabilityInsurance.Option.MaintainCoverage);
            }
        }

        private void WriteTrustAccount(License license, string masterCustomerId)
        {
            if (license.TrustAccount != null && license.LicenseType.LicenseTypeRequirement.TrustAccount != RequirementType.Excluded)
            {
                //get trust account status
                string status = (license.TrustAccount.HandlesTrustAccount) ? "DOES_HANDLE" : "DOES_NOT_HANDLE";

                //update trust account
                WSBA.AMS.MemberManager.SetTrustAccount(masterCustomerId, license.LicensePeriod.EndDate.Year, status);

                if (license.TrustAccount.HandlesTrustAccount && license.TrustAccount.TrustAccountNumbers != null)
                {
                    //get existing ams trust account numbers
                    var amsTrustAccountNumbers = WSBA.AMS.MemberManager.GetTrustAccountNumbers(masterCustomerId, license.LicensePeriod.EndDate.Year);

                    //get list of sequence numbers for the ams trust account numbers
                    var amsSequenceNumbersToDelete = new List<int>();

                    if (amsTrustAccountNumbers != null)
                    {
                        foreach (var amsTrustAccountNumber in amsTrustAccountNumbers) { amsSequenceNumbersToDelete.Add(amsTrustAccountNumber.SequenceNumber); }
                    }

                    //loop through trust account numbers
                    foreach (TrustAccountNumber trustAccountNumber in license.TrustAccount.TrustAccountNumbers)
                    {
                        //update in ams
                        int sequenceNumber = WSBA.AMS.MemberManager.SetTrustAccountNumber(masterCustomerId, license.LicensePeriod.EndDate.Year,
                            trustAccountNumber.AmsSequenceNumber, trustAccountNumber.Bank, trustAccountNumber.Branch, trustAccountNumber.AccountNumber);

                        if (trustAccountNumber.AmsSequenceNumber != sequenceNumber)
                        {
                            trustAccountNumber.AmsSequenceNumber = sequenceNumber;

                            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
                            trustAccountManager.SetTrustAccountNumber(trustAccountNumber);
                        }

                        //remove sequence number from list of trust account numbers to delete
                        if (trustAccountNumber.AmsSequenceNumber > 0) { amsSequenceNumbersToDelete.Remove(trustAccountNumber.AmsSequenceNumber); }
                    }

                    //loop through sequence numbers of trust account numbers to delete
                    foreach (int sequenceNumber in amsSequenceNumbersToDelete)
                    {
                        WSBA.AMS.MemberManager.DeleteTrustAccountNumber(masterCustomerId, sequenceNumber);
                    }
                }
                else
                {
                    WSBA.AMS.MemberManager.DeleteTrustAccountNumbers(masterCustomerId, license.LicensePeriod.EndDate.Year);
                }
            }
        }

        private void WritePrimaryAddress(License license, string masterCustomerId)
        {
            AddressManager addressManager = new AddressManager(_context);
            Address primaryAddress = addressManager.GetPrimaryAddress(license);

            if (primaryAddress != null && license.LicenseType.LicenseTypeRequirement.PrimaryAddress != RequirementType.Excluded)
            {
                var modifyAddress = new WSBA.AMS.ModifyAddress();
                modifyAddress.AddressId = primaryAddress.AmsAddressId;
                modifyAddress.Address1 = primaryAddress.Address1;
                modifyAddress.Address2 = primaryAddress.Address2;
                modifyAddress.City = primaryAddress.City;
                modifyAddress.State = (primaryAddress.State != null) ? primaryAddress.State.AmsCode : null;
                modifyAddress.PostalCode = primaryAddress.ZipCode;
                modifyAddress.CountryCode = primaryAddress.Country.AmsCode;

                WSBA.AMS.AddressManager.SetPrimaryAddress(masterCustomerId, modifyAddress);
            }
        }

        private void WriteHomeAddress(License license, string masterCustomerId)
        {
            string errorMessage = "";

            AddressManager addressManager = new AddressManager(_context);
            Address homeAddress = addressManager.GetHomeAddress(license);

            if (homeAddress != null && license.LicenseType.LicenseTypeRequirement.HomeAddress != RequirementType.Excluded)
            {
                var modifyAddress = new WSBA.AMS.ModifyAddress();
                modifyAddress.AddressId = homeAddress.AmsAddressId;
                modifyAddress.Address1 = homeAddress.Address1;
                modifyAddress.Address2 = homeAddress.Address2;
                modifyAddress.City = homeAddress.City;
                modifyAddress.State = homeAddress.State.AmsCode;
                modifyAddress.PostalCode = homeAddress.ZipCode;
                modifyAddress.CountryCode = homeAddress.Country.AmsCode;

                if (modifyAddress.AddressId > 0)
                {
                    WSBA.AMS.AddressManager.UpdateAddress(modifyAddress, ref errorMessage);
                }
                else
                {
                    WSBA.AMS.AddressManager.CreateHomeAddress(masterCustomerId, modifyAddress, ref errorMessage);
                }
            }
        }

        private void WriteAgentOfServiceAddress(License license, string masterCustomerId)
        {
            string errorMessage = "";

            AddressManager addressManager = new AddressManager(_context);
            Address agentOfServiceAddress = addressManager.GetAgentOfServiceAddress(license);

            if (agentOfServiceAddress != null && license.LicenseType.LicenseTypeRequirement.AgentOfServiceAddress != RequirementType.Excluded)
            {
                var modifyAddress = new WSBA.AMS.ModifyAddress();
                modifyAddress.AddressId = agentOfServiceAddress.AmsAddressId;
                modifyAddress.Address1 = agentOfServiceAddress.Address1;
                modifyAddress.Address2 = agentOfServiceAddress.Address2;
                modifyAddress.City = agentOfServiceAddress.City;
                modifyAddress.State = agentOfServiceAddress.State.AmsCode;
                modifyAddress.PostalCode = agentOfServiceAddress.ZipCode;
                modifyAddress.CountryCode = agentOfServiceAddress.Country.AmsCode;

                if (modifyAddress.AddressId > 0)
                {
                    WSBA.AMS.AddressManager.UpdateAddress(modifyAddress, ref errorMessage);
                }
                else
                {
                    WSBA.AMS.AddressManager.CreateAgentOfServiceAddress(masterCustomerId, modifyAddress, ref errorMessage);
                }
            }
        }

        private void WriteEmail(License license, string masterCustomerId)
        {
            string errorMessage = "";

            if (license.Email != null && license.LicenseType.LicenseTypeRequirement.PrimaryEmail != RequirementType.Excluded)
            {
                WSBA.AMS.CommunicationManager.UpdatePrimaryEmailAddress(masterCustomerId, license.Email.EmailAddress, ref errorMessage);
            }
        }

        private void WritePrimaryPhone(License license, string masterCustomerId)
        {
            string errorMessage = "";

            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumber primaryPhoneNumber = phoneNumberManager.GetPrimaryPhoneNumber(license);

            if (primaryPhoneNumber != null && license.LicenseType.LicenseTypeRequirement.PrimaryPhoneNumber != RequirementType.Excluded)
            {
                var phone = new WSBA.AMS.Phone();
                phone.PhoneCommTypeCode = WSBA.AMS.Enums.CommunicationTypeCode.Phone;

                //loop through comm location codes to find one that matches
                if (primaryPhoneNumber.AmsLocationCode != null)
                {
                    foreach (WSBA.AMS.Enums.CommunicationLocationCode commLocationCode in Enum.GetValues(typeof(WSBA.AMS.Enums.CommunicationLocationCode)))
                    {
                        if (primaryPhoneNumber.AmsLocationCode == WSBA.AMS.Enums.GetEnumDescription(commLocationCode))
                        {
                            phone.PhoneCommLocationCode = commLocationCode;
                        }
                    }
                }

                phone.PhoneCountryCode = primaryPhoneNumber.Country.CountryCode;
                phone.PhoneAreaCode = primaryPhoneNumber.AreaCode;
                phone.PhoneNumber = primaryPhoneNumber.Number;
                phone.PhoneExtension = primaryPhoneNumber.Extension;

                WSBA.AMS.CommunicationManager.UpdatePhoneNumber(masterCustomerId, phone, ref errorMessage);
            }
        }

        private void WriteHomePhone(License license, string masterCustomerId)
        {
            string errorMessage = "";

            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumber homePhoneNumber = phoneNumberManager.GetHomePhoneNumber(license);

            if (homePhoneNumber != null && license.LicenseType.LicenseTypeRequirement.HomePhoneNumber != RequirementType.Excluded)
            {
                var phone = new WSBA.AMS.Phone();
                phone.PhoneCommTypeCode = WSBA.AMS.Enums.CommunicationTypeCode.Phone;
                phone.PhoneCommLocationCode = WSBA.AMS.Enums.CommunicationLocationCode.Home;
                phone.PhoneCountryCode = homePhoneNumber.Country.CountryCode;
                phone.PhoneAreaCode = homePhoneNumber.AreaCode;
                phone.PhoneNumber = homePhoneNumber.Number;
                phone.PhoneExtension = homePhoneNumber.Extension;

                WSBA.AMS.CommunicationManager.UpdateHomePhoneNumber(masterCustomerId, phone, ref errorMessage);
            }
        }

        private void WriteFaxPhone(License license, string masterCustomerId)
        {
            string errorMessage = "";

            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumber faxPhoneNumber = phoneNumberManager.GetFaxPhoneNumber(license);

            if (faxPhoneNumber != null && license.LicenseType.LicenseTypeRequirement.FaxPhoneNumber != RequirementType.Excluded)
            {
                var phone = new WSBA.AMS.Phone();
                phone.PhoneCommTypeCode = WSBA.AMS.Enums.CommunicationTypeCode.Fax;

                //loop through comm location codes to find one that matches
                if (faxPhoneNumber.AmsLocationCode != null)
                {
                    foreach (WSBA.AMS.Enums.CommunicationLocationCode commLocationCode in Enum.GetValues(typeof(WSBA.AMS.Enums.CommunicationLocationCode)))
                    {
                        if (faxPhoneNumber.AmsLocationCode == WSBA.AMS.Enums.GetEnumDescription(commLocationCode))
                        {
                            phone.PhoneCommLocationCode = commLocationCode;
                        }
                    }
                }

                phone.PhoneCountryCode = faxPhoneNumber.Country.CountryCode;
                phone.PhoneAreaCode = faxPhoneNumber.AreaCode;
                phone.PhoneNumber = faxPhoneNumber.Number;
                phone.PhoneExtension = faxPhoneNumber.Extension;

                WSBA.AMS.CommunicationManager.UpdateFaxPhoneNumber(masterCustomerId, phone, ref errorMessage);
            }
        }

        private void WriteGender(License license, string masterCustomerId)
        {
            if (license.Gender != null && license.LicenseType.LicenseTypeRequirement.Gender != RequirementType.Excluded)
            {
                WSBA.AMS.DemographicManager.SetGender(masterCustomerId, license.Gender.Option.AmsCode);
            }
        }

        private void WriteEthnicity(License license, string masterCustomerId)
        {
            if (license.Ethnicity != null && license.LicenseType.LicenseTypeRequirement.Ethnicity != RequirementType.Excluded)
            {
                WSBA.AMS.DemographicManager.SetEthnicity(masterCustomerId, license.Ethnicity.Option.AmsCode);
            }
        }

        private void WriteSexualOrientation(License license, string masterCustomerId)
        {
            if (license.SexualOrientation != null && license.LicenseType.LicenseTypeRequirement.SexualOrientation != RequirementType.Excluded)
            {
                WSBA.AMS.DemographicManager.SetSexualOrientation(masterCustomerId, license.SexualOrientation.Option.AmsCode);
            }
        }

        private void WriteDisability(License license, string masterCustomerId)
        {
            if (license.Disability != null && license.LicenseType.LicenseTypeRequirement.Disability != RequirementType.Excluded)
            {
                WSBA.AMS.DemographicManager.SetDisability(masterCustomerId, license.Disability.Option.AmsCode);
            }
        }

        private void WriteAreasOfPractice(License license, string masterCustomerId)
        {
            if (license.AreasOfPractice != null && license.LicenseType.LicenseTypeRequirement.AreasOfPractice != RequirementType.Excluded)
            {
                List<string> areaOfPracticeAmsCodes = new List<string>();

                foreach (AreaOfPractice areaOfPractice in license.AreasOfPractice)
                {
                    areaOfPracticeAmsCodes.Add(areaOfPractice.Option.AmsCode);
                }

                WSBA.AMS.DemographicManager.SetAreasOfPractice(masterCustomerId, areaOfPracticeAmsCodes);
            }
        }

        private void WriteFirmSize(License license, string masterCustomerId)
        {
            if (license.FirmSize != null && license.LicenseType.LicenseTypeRequirement.FirmSize != RequirementType.Excluded)
            {
                WSBA.AMS.DemographicManager.SetFirmSize(masterCustomerId, license.FirmSize.Option.AmsCode);
            }
        }

        private void WriteLanguages(License license, string masterCustomerId)
        {
            if (license.Languages != null && license.LicenseType.LicenseTypeRequirement.Languages != RequirementType.Excluded)
            {
                List<string> languageAmsCodes = new List<string>();

                foreach (Language language in license.Languages)
                {
                    languageAmsCodes.Add(language.Option.AmsCode);
                }

                WSBA.AMS.DemographicManager.SetLanguages(masterCustomerId, languageAmsCodes);
            }
        }
    }
}
