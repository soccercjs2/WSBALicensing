using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Customers;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.FirmSizes;
using Licensing.Domain.Genders;
using Licensing.Domain.Judicial;
using Licensing.Domain.Languages;
using Licensing.Domain.Licenses;
using Licensing.Domain.PracticeAreas;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using Licensing.Domain.SexualOrientations;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class AmsUpdateManager
    {
        private LicensingContext _context;

        public AmsUpdateManager(LicensingContext context)
        {
            _context = context;
        }

        public void UpdateCustomer(ref Customer customer)
        {
            string masterCustomerId = "000000000000".Substring(0, 12 - customer.BarNumber.Length) + customer.BarNumber;

            var amsCustomer = WSBA.AMS.CustomerManager.GetCustomer(masterCustomerId);
            customer.FirstName = amsCustomer.FirstName;
            customer.LastName = amsCustomer.LastName;
        }

        public void UpdateLicense(ref License license)
        {
            //create master customer id
            string masterCustomerId = "000000000000".Substring(0, 12 - license.Customer.BarNumber.Length) + license.Customer.BarNumber;
            DateTime nextAmsUpdate = DateTime.Now;

            UpdateMemberType(ref license, masterCustomerId, nextAmsUpdate);
            UpdateFinancialResponsibility(ref license, masterCustomerId, nextAmsUpdate);
            UpdateJudicialPosition(ref license, masterCustomerId, nextAmsUpdate);
            UpdatePracticeAreas(ref license, masterCustomerId, nextAmsUpdate);
            UpdateProBono(ref license, masterCustomerId, nextAmsUpdate);
            UpdateProfessionalLiabilityInsurance(ref license, masterCustomerId, nextAmsUpdate);
            UpdateTrustAccount(ref license, masterCustomerId, nextAmsUpdate);
            UpdateTrustAccountNumbers(ref license, masterCustomerId, nextAmsUpdate);
            UpdatePrimaryAddress(ref license, masterCustomerId, nextAmsUpdate);
            UpdateHomeAddress(ref license, masterCustomerId, nextAmsUpdate);
            UpdateAgentOfServiceAddress(ref license, masterCustomerId, nextAmsUpdate);
            UpdateEmail(ref license, masterCustomerId, nextAmsUpdate);
            UpdatePrimaryPhone(ref license, masterCustomerId, nextAmsUpdate);
            UpdateHomePhone(ref license, masterCustomerId, nextAmsUpdate);
            UpdateFaxPhone(ref license, masterCustomerId, nextAmsUpdate);
            UpdateGender(ref license, masterCustomerId, nextAmsUpdate);
            UpdateEthnicity(ref license, masterCustomerId, nextAmsUpdate);
            UpdateSexualOrientation(ref license, masterCustomerId, nextAmsUpdate);
            UpdateDisability(ref license, masterCustomerId, nextAmsUpdate);
            UpdateAreasOfPractice(ref license, masterCustomerId, nextAmsUpdate);
            UpdateFirmSize(ref license, masterCustomerId, nextAmsUpdate);
            UpdateLanguages(ref license, masterCustomerId, nextAmsUpdate);

            LicenseManager licenseManager = new LicenseManager(_context);
            licenseManager.SetLastAmsUpdate(license, nextAmsUpdate);
        }

        private void UpdateMemberType(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsCurrentMemberStatus = WSBA.AMS.MemberManager.GetMemberStatusByDate(masterCustomerId, DateTime.Today);

            //update license type
            if (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsCurrentMemberStatus.AddedDate && amsCurrentMemberStatus.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsCurrentMemberStatus.ModifiedDate && amsCurrentMemberStatus.ModifiedDate < nextAmsUpdate))
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                LicenseType currentLicenseType = licenseTypeManager.GetLicenseType(amsCurrentMemberStatus.MemberTypeDescr.ToString());
                licenseTypeManager.ChangeLicenseType(license, currentLicenseType);

                if (license.LastAmsUpdate == null)
                {
                    var amsLicenseStartMemberStatus = WSBA.AMS.MemberManager.GetMemberStatusByDate(masterCustomerId, license.LicensePeriod.StartDate);
                    LicenseType licenseStartLicenseType = licenseTypeManager.GetLicenseType(amsLicenseStartMemberStatus.MemberType.ToString());
                    licenseTypeManager.ChangePreviousLicenseType(license, licenseStartLicenseType);
                }
            }
        }

        private void UpdateFinancialResponsibility(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsFinancialResponsibility = WSBA.AMS.MemberManager.GetFinancialResponsibility(masterCustomerId, license.LicensePeriod.EndDate.Year);

            //preload last year's financial responsibility
            if (amsFinancialResponsibility == null && license.LastAmsUpdate == null)
            {
                amsFinancialResponsibility = WSBA.AMS.MemberManager.GetFinancialResponsibility(masterCustomerId, license.LicensePeriod.EndDate.Year - 1);
            }

            //update financial responsibility
            if (amsFinancialResponsibility != null && 
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsFinancialResponsibility.AddedDate && amsFinancialResponsibility.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsFinancialResponsibility.ModifiedDate && amsFinancialResponsibility.ModifiedDate < nextAmsUpdate)))
            {
                FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
                CoveredByOption option = financialResponsibilityManager.GetOption(amsFinancialResponsibility.CoveredBy);
                financialResponsibilityManager.SetFinancialResponsibility(license, amsFinancialResponsibility.Company, amsFinancialResponsibility.PolicyNumber, option.CoveredByOptionId);
            }
        }

        private void UpdateJudicialPosition(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsJudicialPosition = WSBA.AMS.DemographicManager.GetActiveJudicialPosition(masterCustomerId);

            //update judicial position
            if (amsJudicialPosition != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsJudicialPosition.AddedDate && amsJudicialPosition.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsJudicialPosition.ModifiedDate && amsJudicialPosition.ModifiedDate < nextAmsUpdate)))
            {
                JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
                JudicialPositionOption option = judicialPositionManager.GetOption(amsJudicialPosition.DemographicSubcode);
                judicialPositionManager.SetJudicialPosition(license, option);
            }
        }

        private void UpdatePracticeAreas(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsPracticeAreas = WSBA.AMS.MemberManager.GetActivePracticeAreas(masterCustomerId);

            if (amsPracticeAreas != null)
            {
                PracticeAreaManager practiceAreaManager = new PracticeAreaManager(_context);

                foreach (var amsPracticeArea in amsPracticeAreas)
                {
                    //update judicial position
                    if (license.LastAmsUpdate == null || 
                        (license.LastAmsUpdate <= amsPracticeArea.AddedDate && amsPracticeArea.AddedDate < nextAmsUpdate) || 
                        (license.LastAmsUpdate <= amsPracticeArea.ModifiedDate && amsPracticeArea.ModifiedDate < nextAmsUpdate))
                    {
                        PracticeAreaOption option = practiceAreaManager.GetOption(amsPracticeArea.PracticeArea);

                        if (amsPracticeArea.StartDate <= DateTime.Today && (amsPracticeArea.EndDate == null || amsPracticeArea.EndDate > DateTime.Today))
                        {
                            if (!practiceAreaManager.HasPracticeArea(license, option))
                            {
                                practiceAreaManager.AddPracticeArea(license, option);
                            }
                        }
                        else
                        {
                            practiceAreaManager.DeletePracticeArea(license, option);
                        }
                    }
                }
            }
        }

        private void UpdateProBono(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsProBono = WSBA.AMS.MemberManager.GetProBono(masterCustomerId, license.LicensePeriod.EndDate.Year);

            //update financial responsibility
            if (amsProBono != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsProBono.AddedDate && amsProBono.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsProBono.ModifiedDate && amsProBono.ModifiedDate < nextAmsUpdate)))
            {
                bool provededService = (amsProBono.FreeServiceHours > 0 || amsProBono.LimitedFeeServiceHours > 0);

                ProBonoManager proBonoManager = new ProBonoManager(_context);
                proBonoManager.SetProBono(license, provededService, amsProBono.FreeServiceHours, amsProBono.LimitedFeeServiceHours, amsProBono.Anonymous);
            }
        }

        private void UpdateProfessionalLiabilityInsurance(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsProfessionalLiabilityInsurance = WSBA.AMS.MemberManager.GetInsuranceDisclosure(masterCustomerId, license.LicensePeriod.EndDate.Year);

            //preload last year's financial responsibility
            if (amsProfessionalLiabilityInsurance == null && license.LastAmsUpdate == null)
            {
                amsProfessionalLiabilityInsurance = WSBA.AMS.MemberManager.GetInsuranceDisclosure(masterCustomerId, license.LicensePeriod.EndDate.Year - 1);
            }

            //update financial responsibility
            if (amsProfessionalLiabilityInsurance != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsProfessionalLiabilityInsurance.AddedDate && amsProfessionalLiabilityInsurance.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsProfessionalLiabilityInsurance.ModifiedDate && amsProfessionalLiabilityInsurance.ModifiedDate < nextAmsUpdate)))
            {
                ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
                ProfessionalLiabilityInsuranceOption option = professionalLiabilityInsuranceManager.GetOption(
                    amsProfessionalLiabilityInsurance.PrivatePractice, amsProfessionalLiabilityInsurance.CurrentlyInsured, amsProfessionalLiabilityInsurance.MaintainCoverage);

                professionalLiabilityInsuranceManager.SetProfessionalLiabilityInsuranceOption(license, option.ProfessionalLiabilityInsuranceOptionId);
            }
        }

        private void UpdateTrustAccount(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsTrustAccount = WSBA.AMS.MemberManager.GetTrustAccount(masterCustomerId, license.LicensePeriod.EndDate.Year);

            //preload last year's trust account
            if (amsTrustAccount == null && license.LastAmsUpdate == null)
            {
                amsTrustAccount = WSBA.AMS.MemberManager.GetTrustAccount(masterCustomerId, license.LicensePeriod.EndDate.Year - 1);
            }

            //update trust account
            if (amsTrustAccount != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsTrustAccount.AddedDate && amsTrustAccount.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsTrustAccount.ModifiedDate && amsTrustAccount.ModifiedDate < nextAmsUpdate)))
            {
                TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
                trustAccountManager.SetTrustAccount(license, amsTrustAccount.Status == "DOES_HANDLE");
            }
        }

        private void UpdateTrustAccountNumbers(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsTrustAccountNumbers = WSBA.AMS.MemberManager.GetTrustAccountNumbers(masterCustomerId, license.LicensePeriod.EndDate.Year);

            //preload last year's trust acocount numbers
            if (amsTrustAccountNumbers == null && license.LastAmsUpdate == null)
            {
                amsTrustAccountNumbers = WSBA.AMS.MemberManager.GetTrustAccountNumbers(masterCustomerId, license.LicensePeriod.EndDate.Year - 1);
            }

            //update trust account numbers
            if (amsTrustAccountNumbers != null)
            {
                TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
                List<int> amsSequenceNumbers = new List<int>();

                foreach (var amsTrustAccountNumber in amsTrustAccountNumbers)
                {
                    amsSequenceNumbers.Add(amsTrustAccountNumber.SequenceNumber);
                    
                    //update trust account number
                    if (license.LastAmsUpdate == null || 
                        (license.LastAmsUpdate <= amsTrustAccountNumber.AddedDate && amsTrustAccountNumber.AddedDate < nextAmsUpdate) || 
                        (license.LastAmsUpdate <= amsTrustAccountNumber.ModifiedDate && amsTrustAccountNumber.ModifiedDate < nextAmsUpdate))
                    {
                        TrustAccountNumber trustAccountNumber = trustAccountManager.GetTrustAccountNumber(license, amsTrustAccountNumber.SequenceNumber);
                        if (trustAccountNumber == null)
                        {
                            trustAccountNumber = new TrustAccountNumber();
                            trustAccountNumber.TrustAccountId = license.TrustAccount.TrustAccountId;
                        }

                        trustAccountNumber.Bank = amsTrustAccountNumber.Insitution;
                        trustAccountNumber.Branch = amsTrustAccountNumber.Branch;
                        trustAccountNumber.AccountNumber = amsTrustAccountNumber.AccountNumber;
                        trustAccountNumber.AmsSequenceNumber = amsTrustAccountNumber.SequenceNumber;

                        trustAccountManager.SetTrustAccountNumber(trustAccountNumber);
                    }
                }

                foreach (TrustAccountNumber trustAccountNumber in license.TrustAccount.TrustAccountNumbers)
                {
                    if (trustAccountNumber.AmsSequenceNumber > 0 && !amsSequenceNumbers.Contains(trustAccountNumber.AmsSequenceNumber))
                    {
                        trustAccountManager.DeleteTrustAccountNumber(trustAccountNumber.TrustAccountNumberId);
                    }
                }
            }
        }

        private void UpdatePrimaryAddress(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsPrimaryAddress = WSBA.AMS.AddressManager.GetPrimaryAddress(masterCustomerId);

            //update primary address
            if (amsPrimaryAddress != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsPrimaryAddress.AddedDate && amsPrimaryAddress.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsPrimaryAddress.ModifiedDate && amsPrimaryAddress.ModifiedDate < nextAmsUpdate)))
            {
                AddressManager addressManager = new AddressManager(_context);
                Address primaryAddress = addressManager.GetPrimaryAddress(license);

                if (primaryAddress == null)
                {
                    primaryAddress = new Address()
                    {
                        LicenseId = license.LicenseId,
                        AddressType = addressManager.GetAddressType("OFFICE")
                    };
                }

                primaryAddress.AmsAddressId = (int)amsPrimaryAddress.AddressId;
                primaryAddress.Address1 = amsPrimaryAddress.Address1;
                primaryAddress.Address2 = amsPrimaryAddress.Address2;
                primaryAddress.City = amsPrimaryAddress.City;
                primaryAddress.State = addressManager.GetAddressState(amsPrimaryAddress.CountryCode, amsPrimaryAddress.State);
                primaryAddress.ZipCode = amsPrimaryAddress.PostalCode;
                primaryAddress.Country = addressManager.GetAddressCountry(amsPrimaryAddress.CountryCode);
                primaryAddress.CongressionalDistrict = amsPrimaryAddress.CongressionalDistrict;

                addressManager.SetAddress(primaryAddress);
            }
        }

        private void UpdateHomeAddress(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsHomeAddress = WSBA.AMS.AddressManager.GetHomeAddress(masterCustomerId);

            //update home address
            if (amsHomeAddress != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsHomeAddress.AddedDate && amsHomeAddress.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsHomeAddress.ModifiedDate && amsHomeAddress.ModifiedDate < nextAmsUpdate)))
            {
                AddressManager addressManager = new AddressManager(_context);
                Address homeAddress = addressManager.GetHomeAddress(license);

                if (homeAddress == null)
                {
                    homeAddress = new Address()
                    {
                        LicenseId = license.LicenseId,
                        AddressType = addressManager.GetAddressType("HOME")
                    };
                }

                homeAddress.Address1 = amsHomeAddress.Address1;
                homeAddress.Address2 = amsHomeAddress.Address2;
                homeAddress.City = amsHomeAddress.City;
                homeAddress.State = addressManager.GetAddressState(amsHomeAddress.CountryCode, amsHomeAddress.State);
                homeAddress.ZipCode = amsHomeAddress.PostalCode;
                homeAddress.Country = addressManager.GetAddressCountry(amsHomeAddress.CountryCode);
                homeAddress.CongressionalDistrict = amsHomeAddress.CongressionalDistrict;

                addressManager.SetAddress(homeAddress);
            }
        }

        private void UpdateAgentOfServiceAddress(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsAgentOfServiceAddress = WSBA.AMS.AddressManager.GetAgentOfServiceAddress(masterCustomerId);

            //update agent of service address
            if (amsAgentOfServiceAddress != null && amsAgentOfServiceAddress.Address1 != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsAgentOfServiceAddress.AddedDate && amsAgentOfServiceAddress.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsAgentOfServiceAddress.ModifiedDate && amsAgentOfServiceAddress.ModifiedDate < nextAmsUpdate)))
            {
                AddressManager addressManager = new AddressManager(_context);
                Address agentOfServiceAddress = addressManager.GetAgentOfServiceAddress(license);

                if (agentOfServiceAddress == null)
                {
                    agentOfServiceAddress = new Address()
                    {
                        LicenseId = license.LicenseId,
                        AddressType = addressManager.GetAddressType("AGENTOFSERVICE")
                    };
                }

                agentOfServiceAddress.Address1 = amsAgentOfServiceAddress.Address1;
                agentOfServiceAddress.Address2 = amsAgentOfServiceAddress.Address2;
                agentOfServiceAddress.City = amsAgentOfServiceAddress.City;
                agentOfServiceAddress.State = addressManager.GetAddressState(amsAgentOfServiceAddress.CountryCode, amsAgentOfServiceAddress.State);
                agentOfServiceAddress.ZipCode = amsAgentOfServiceAddress.PostalCode;
                agentOfServiceAddress.Country = addressManager.GetAddressCountry(amsAgentOfServiceAddress.CountryCode);
                agentOfServiceAddress.CongressionalDistrict = amsAgentOfServiceAddress.CongressionalDistrict;

                addressManager.SetAddress(agentOfServiceAddress);
            }
        }

        private void UpdateEmail(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsEmail = WSBA.AMS.CommunicationManager.GetPrimaryEmailAddress(masterCustomerId);
            EmailManager emailmanager = new EmailManager(_context);
            Email email = license.Email;

            //update primary email address
            if (amsEmail != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsEmail.AddedDate && amsEmail.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsEmail.ModifiedDate && amsEmail.ModifiedDate < nextAmsUpdate)))
            {
                if (email == null)
                {
                    email = new Email()
                    {
                        LicenseId = license.LicenseId
                    };
                }

                email.EmailAddress = amsEmail.PrimaryEmailAddress;

                emailmanager.SetEmail(license, email);
            }
        }

        private void UpdatePrimaryPhone(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsPrimaryPhoneNumber = WSBA.AMS.CommunicationManager.GetPrimaryPhone(masterCustomerId);

            //update primary phone number
            if (amsPrimaryPhoneNumber != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsPrimaryPhoneNumber.AddedDate && amsPrimaryPhoneNumber.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsPrimaryPhoneNumber.ModifiedDate && amsPrimaryPhoneNumber.ModifiedDate < nextAmsUpdate)))
            {
                PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
                PhoneNumber primaryPhoneNumber = phoneNumberManager.GetPrimaryPhoneNumber(license);

                if (primaryPhoneNumber == null)
                {
                    primaryPhoneNumber = new PhoneNumber()
                    {
                        LicenseId = license.LicenseId,
                        PhoneNumberType = phoneNumberManager.GetPhoneNumberType("Primary")
                    };
                }

                primaryPhoneNumber.AmsLocationCode = WSBA.AMS.Enums.GetEnumDescription(amsPrimaryPhoneNumber.PhoneCommLocationCode);
                primaryPhoneNumber.Country = phoneNumberManager.GetCountry(amsPrimaryPhoneNumber.PhoneCountryCode);
                primaryPhoneNumber.AreaCode = amsPrimaryPhoneNumber.PhoneAreaCode;
                primaryPhoneNumber.Number = amsPrimaryPhoneNumber.PhoneNumber;

                if (amsPrimaryPhoneNumber.PhoneExtension != null && amsPrimaryPhoneNumber.PhoneExtension.Length > 0)
                {
                    primaryPhoneNumber.Extension = amsPrimaryPhoneNumber.PhoneExtension;
                }

                phoneNumberManager.SetPhoneNumber(primaryPhoneNumber);
            }
        }

        private void UpdateHomePhone(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsHomePhoneNumber = WSBA.AMS.CommunicationManager.GetHomePhone(masterCustomerId);

            //update home phone number
            if (amsHomePhoneNumber != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsHomePhoneNumber.AddedDate && amsHomePhoneNumber.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsHomePhoneNumber.ModifiedDate && amsHomePhoneNumber.ModifiedDate < nextAmsUpdate)))
            {
                PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
                PhoneNumber homePhoneNumber = phoneNumberManager.GetHomePhoneNumber(license);

                if (homePhoneNumber == null)
                {
                    homePhoneNumber = new PhoneNumber()
                    {
                        LicenseId = license.LicenseId,
                        PhoneNumberType = phoneNumberManager.GetPhoneNumberType("Home")
                    };
                }

                homePhoneNumber.Country = phoneNumberManager.GetCountry(amsHomePhoneNumber.PhoneCountryCode);
                homePhoneNumber.AreaCode = amsHomePhoneNumber.PhoneAreaCode;
                homePhoneNumber.Number = amsHomePhoneNumber.PhoneNumber;

                if (amsHomePhoneNumber.PhoneExtension != null && amsHomePhoneNumber.PhoneExtension.Length > 0)
                {
                    homePhoneNumber.Extension = amsHomePhoneNumber.PhoneExtension;
                }

                phoneNumberManager.SetPhoneNumber(homePhoneNumber);
            }
        }

        private void UpdateFaxPhone(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsFaxPhoneNumber = WSBA.AMS.CommunicationManager.GetFax(masterCustomerId);

            //update fax number
            if (amsFaxPhoneNumber != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsFaxPhoneNumber.AddedDate && amsFaxPhoneNumber.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsFaxPhoneNumber.ModifiedDate && amsFaxPhoneNumber.ModifiedDate < nextAmsUpdate)))
            {
                PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
                PhoneNumber homePhoneNumber = phoneNumberManager.GetFaxPhoneNumber(license);

                if (homePhoneNumber == null)
                {
                    homePhoneNumber = new PhoneNumber()
                    {
                        LicenseId = license.LicenseId,
                        PhoneNumberType = phoneNumberManager.GetPhoneNumberType("Fax")
                    };
                }

                homePhoneNumber.Country = phoneNumberManager.GetCountry(amsFaxPhoneNumber.PhoneCountryCode);
                homePhoneNumber.AreaCode = amsFaxPhoneNumber.PhoneAreaCode;
                homePhoneNumber.Number = amsFaxPhoneNumber.PhoneNumber;

                if (amsFaxPhoneNumber.PhoneExtension != null && amsFaxPhoneNumber.PhoneExtension.Length > 0)
                {
                    homePhoneNumber.Extension = amsFaxPhoneNumber.PhoneExtension;
                }

                phoneNumberManager.SetPhoneNumber(homePhoneNumber);
            }
        }

        private void UpdateGender(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsGender = WSBA.AMS.DemographicManager.GetActiveGender(masterCustomerId);

            //update judicial position
            if (amsGender != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsGender.AddedDate && amsGender.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsGender.ModifiedDate && amsGender.ModifiedDate < nextAmsUpdate)))
            {
                GenderManager genderManager = new GenderManager(_context);
                GenderOption option = genderManager.GetOption(amsGender.DemographicSubcode);
                genderManager.SetGender(license, option);
            }
        }

        private void UpdateEthnicity(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsEthnicity = WSBA.AMS.DemographicManager.GetActiveEthnicity(masterCustomerId);

            //update ethnicity
            if (amsEthnicity != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsEthnicity.AddedDate && amsEthnicity.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsEthnicity.ModifiedDate && amsEthnicity.ModifiedDate < nextAmsUpdate)))
            {
                EthnicityManager ethnicityManager = new EthnicityManager(_context);
                EthnicityOption option = ethnicityManager.GetOption(amsEthnicity.DemographicSubcode);
                ethnicityManager.SetEthnicity(license, option);
            }
        }

        private void UpdateSexualOrientation(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsSexualOrientation = WSBA.AMS.DemographicManager.GetActiveSexualOrientation(masterCustomerId);

            //update sexual orientation
            if (amsSexualOrientation != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsSexualOrientation.AddedDate && amsSexualOrientation.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsSexualOrientation.ModifiedDate && amsSexualOrientation.ModifiedDate < nextAmsUpdate)))
            {
                SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);
                SexualOrientationOption option = sexualOrientationManager.GetOption(amsSexualOrientation.DemographicSubcode);
                sexualOrientationManager.SetSexualOrientation(license, option);
            }
        }

        private void UpdateDisability(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsDisability = WSBA.AMS.DemographicManager.GetActiveDisability(masterCustomerId);

            //update disability
            if (amsDisability != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsDisability.AddedDate && amsDisability.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsDisability.ModifiedDate && amsDisability.ModifiedDate < nextAmsUpdate)))
            {
                DisabilityManager disabilityManager = new DisabilityManager(_context);
                DisabilityOption option = disabilityManager.GetOption(amsDisability.DemographicSubcode);
                disabilityManager.SetDisability(license, option);
            }
        }

        private void UpdateAreasOfPractice(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsAreasOfPractice = WSBA.AMS.DemographicManager.GetActiveAreasOfPractice(masterCustomerId);

            //update areas of practice
            if (amsAreasOfPractice != null)
            {
                AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);
                
                foreach (var amsAreaOfPractice in amsAreasOfPractice)
                {
                    //add area of practice
                    if (license.LastAmsUpdate == null || 
                        (license.LastAmsUpdate <= amsAreaOfPractice.AddedDate && amsAreaOfPractice.AddedDate < nextAmsUpdate) || 
                        (license.LastAmsUpdate <= amsAreaOfPractice.ModifiedDate && amsAreaOfPractice.ModifiedDate < nextAmsUpdate))
                    {
                        AreaOfPractice areaOfPractice = areaOfPracticeManager.GetAreaOfPractice(license, amsAreaOfPractice.DemographicSubcode);

                        if (areaOfPractice == null && amsAreaOfPractice.EndDate == DateTime.MinValue)
                        {
                            AreaOfPracticeOption option = areaOfPracticeManager.GetOption(amsAreaOfPractice.DemographicSubcode);
                            areaOfPracticeManager.AddAreaOfPractice(license, option.AreaOfPracticeOptionId);
                        }

                        if (areaOfPractice != null && amsAreaOfPractice.EndDate > DateTime.MinValue && amsAreaOfPractice.EndDate <= DateTime.Today)
                        {
                            areaOfPracticeManager.DeleteAreaOfPractice(license, areaOfPractice.Option.AreaOfPracticeOptionId);
                        }
                    }
                }

                //delete areas of practice
                //foreach (AreaOfPractice areaOfPractice in license.AreasOfPractice)
                //{
                //    bool existsInAms = false;

                //    foreach (var amsAreaOfPractice in amsAreasOfPractice)
                //    {
                //        if (amsAreaOfPractice.DemographicSubcode == areaOfPractice.Option.AmsCode)
                //        {
                //            existsInAms = true;
                //        }
                //    }

                //    if (!existsInAms)
                //    {
                //        areaOfPracticeManager.DeleteAreaOfPractice(license, areaOfPractice.Option.AreaOfPracticeOptionId);
                //    }
                //}
            }
        }

        private void UpdateFirmSize(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsFirmSize = WSBA.AMS.DemographicManager.GetActiveFirmSize(masterCustomerId);

            //update disability
            if (amsFirmSize != null &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsFirmSize.AddedDate && amsFirmSize.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsFirmSize.ModifiedDate && amsFirmSize.ModifiedDate < nextAmsUpdate)))
            {
                FirmSizeManager firmSizeManager = new FirmSizeManager(_context);
                FirmSizeOption option = firmSizeManager.GetOption(amsFirmSize.DemographicSubcode);
                firmSizeManager.SetFirmSize(license, option);
            }
        }

        private void UpdateLanguages(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsLanguages = WSBA.AMS.DemographicManager.GetActiveLanguages(masterCustomerId);

            //update languages
            if (amsLanguages != null)
            {
                LanguageManager languageManager = new LanguageManager(_context);

                foreach (var amsLanguage in amsLanguages)
                {
                    //add area of practice
                    if (license.LastAmsUpdate == null || 
                        (license.LastAmsUpdate <= amsLanguage.AddedDate && amsLanguage.AddedDate < nextAmsUpdate) || 
                        (license.LastAmsUpdate <= amsLanguage.ModifiedDate && amsLanguage.ModifiedDate < nextAmsUpdate))
                    {
                        Language language = languageManager.GetLanguage(license, amsLanguage.DemographicSubcode);

                        if (language == null && amsLanguage.EndDate == DateTime.MinValue)
                        {
                            LanguageOption option = languageManager.GetOption(amsLanguage.DemographicSubcode);
                            languageManager.AddLanguage(license, option.LanguageOptionId);
                        }

                        if (language != null && amsLanguage.EndDate > DateTime.MinValue && amsLanguage.EndDate <= DateTime.Today)
                        {
                            languageManager.DeleteLanguage(license, language.Option.LanguageOptionId);
                        }
                    }
                }

                //delete areas of practice
                //foreach (Language language in license.Languages)
                //{
                //    bool existsInAms = false;

                //    foreach (var amsLanguage in amsLanguages)
                //    {
                //        if (amsLanguage.DemographicSubcode == language.Option.AmsCode)
                //        {
                //            existsInAms = true;
                //        }
                //    }

                //    if (!existsInAms)
                //    {
                //        languageManager.DeleteLanguage(license, language.Option.LanguageOptionId);
                //    }
                //}
            }
        }
    }
}
