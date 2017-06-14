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
using Licensing.Domain.Orders;
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
            customer.WaAdmissionDate = WSBA.AMS.MemberManager.GetWSBAAdmissionDate(masterCustomerId);
            customer.EarliestAdmissionDate = WSBA.AMS.MemberManager.GetEarliestAdmissionDate(masterCustomerId);
        }

        public void UpdateLicense(ref License license)
        {
            //create master customer id
            string masterCustomerId = "000000000000".Substring(0, 12 - license.Customer.BarNumber.Length) + license.Customer.BarNumber;
            DateTime nextAmsUpdate = DateTime.Now;

            UpdateMemberType(ref license, masterCustomerId, nextAmsUpdate);
            UpdateEmployer(ref license, masterCustomerId, nextAmsUpdate);
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
            UpdateSections(ref license, masterCustomerId, nextAmsUpdate);
            UpdateLicenseFeeExemption(ref license, masterCustomerId, nextAmsUpdate);

            LicenseManager licenseManager = new LicenseManager(_context);
            licenseManager.SetLastAmsUpdate(license, nextAmsUpdate);
        }

        public void UpdateOrders(ref License license)
        {
            //create master customer id
            string masterCustomerId = "000000000000".Substring(0, 12 - license.Customer.BarNumber.Length) + license.Customer.BarNumber;

            UpdateLicenseOrder(ref license, masterCustomerId);
            UpdateSectionOrder(ref license, masterCustomerId);
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
                LicenseType currentLicenseType = licenseTypeManager.GetLicenseType(amsCurrentMemberStatus.MemberTypeCode);
                licenseTypeManager.ChangeLicenseType(license, currentLicenseType);

                if (license.LastAmsUpdate == null)
                {
                    var amsLicenseStartMemberStatus = WSBA.AMS.MemberManager.GetMemberStatusByDate(masterCustomerId, license.LicensePeriod.StartDate);
                    LicenseType licenseStartLicenseType = licenseTypeManager.GetLicenseType(amsLicenseStartMemberStatus.MemberTypeCode);
                    licenseTypeManager.ChangePreviousLicenseType(license, licenseStartLicenseType);
                }
            }
        }

        private void UpdateEmployer(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsEmployer = WSBA.AMS.MemberManager.GetBulkPayor(masterCustomerId);

            //update license type
            if (amsEmployer != null)
            {
                if (license.LastAmsUpdate == null ||
                    (license.LastAmsUpdate <= amsEmployer.AddedDate && amsEmployer.AddedDate < nextAmsUpdate) ||
                    (license.LastAmsUpdate <= amsEmployer.ModifiedDate && amsEmployer.ModifiedDate < nextAmsUpdate))
                {
                    EmployerManager employerManager = new EmployerManager(_context);
                    employerManager.SetEmployer(license, int.Parse(amsEmployer.MasterCustomerId), amsEmployer.Name);
                }
            }
            else if (amsEmployer == null && license.Employer != null)
            {
                EmployerManager employerManager = new EmployerManager(_context);
                employerManager.DeleteEmployer(license);
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
                financialResponsibilityManager.SetFinancialResponsibility(license, (int)amsFinancialResponsibility.FinancialResponsibilityId, amsFinancialResponsibility.Company, amsFinancialResponsibility.PolicyNumber, option.CoveredByOptionId);
            }
            else if (amsFinancialResponsibility == null && license.FinancialResponsibility != null && license.FinancialResponsibility.AmsFinancialResponsibilityId > 0)
            {
                FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
                financialResponsibilityManager.DeleteFinancialResponsibility(license);
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
            else if (amsJudicialPosition == null && license.JudicialPosition != null && license.LastAmsUpdate != null &&
                WSBA.AMS.DemographicManager.HasJudicialPositionRecentlyExpired(masterCustomerId, (DateTime)license.LastAmsUpdate))
            {
                JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
                judicialPositionManager.DeleteJudicialPosition(license);
            }
        }

        private void UpdatePracticeAreas(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsPracticeAreas = WSBA.AMS.MemberManager.GetPracticeAreas(masterCustomerId);

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

                        if (amsPracticeArea.StartDate <= DateTime.Today && (amsPracticeArea.EndDate == DateTime.MinValue || amsPracticeArea.EndDate > DateTime.Today))
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
                proBonoManager.SetProBono(license, amsProBono.SequenceNumber, provededService, amsProBono.FreeServiceHours, amsProBono.LimitedFeeServiceHours, amsProBono.Anonymous);
            }
            else if (amsProBono == null && license.ProBono != null && license.ProBono.AmsSequenceNumber > 0)
            {
                ProBonoManager proBonoManager = new ProBonoManager(_context);
                proBonoManager.DeleteProBono(license);
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

                professionalLiabilityInsuranceManager.SetProfessionalLiabilityInsurance(license, amsProfessionalLiabilityInsurance.SequenceNumber, option.ProfessionalLiabilityInsuranceOptionId);
            }
            else if (amsProfessionalLiabilityInsurance == null && license.ProfessionalLiabilityInsurance != null && license.ProfessionalLiabilityInsurance.AmsSequenceNumber > 0)
            {
                ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
                professionalLiabilityInsuranceManager.DeleteProfessionalLiabilityInsurance(license);
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
                trustAccountManager.SetTrustAccount(license, amsTrustAccount.Status == "DOES_HANDLE", amsTrustAccount.SequenceNumber);
            }
            else if (amsTrustAccount == null && license.TrustAccount != null && license.TrustAccount.AmsSequenceNumber > 0)
            {
                TrustAccountManager trustAccountManager = new TrustAccountManager(_context);

                if (license.TrustAccount.TrustAccountNumbers != null)
                {
                    foreach (TrustAccountNumber trustAccountNumber in license.TrustAccount.TrustAccountNumbers.ToList())
                    {
                        trustAccountManager.DeleteTrustAccountNumber(trustAccountNumber.TrustAccountId);
                    }
                }

                trustAccountManager.DeleteTrustAccount(license);
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

                if (license.TrustAccount != null && license.TrustAccount.TrustAccountNumbers != null)
                {
                    foreach (TrustAccountNumber trustAccountNumber in license.TrustAccount.TrustAccountNumbers)
                    {
                        if (trustAccountNumber.AmsSequenceNumber > 0 && !amsSequenceNumbers.Contains(trustAccountNumber.AmsSequenceNumber))
                        {
                            trustAccountManager.DeleteTrustAccountNumber(trustAccountNumber.TrustAccountNumberId);
                        }
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
            else if (amsPrimaryAddress == null || amsPrimaryAddress.AddressTypeCode == WSBA.AMS.Enums.AddressTypeCode.NONE)
            {
                AddressManager addressManager = new AddressManager(_context);
                Address primaryAddress = addressManager.GetPrimaryAddress(license);

                if (primaryAddress != null && primaryAddress.AmsAddressId > 0)
                {
                    addressManager.DeleteAddress(primaryAddress);
                }
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

                homeAddress.AmsAddressId = (int)amsHomeAddress.AddressId;
                homeAddress.Address1 = amsHomeAddress.Address1;
                homeAddress.Address2 = amsHomeAddress.Address2;
                homeAddress.City = amsHomeAddress.City;
                homeAddress.State = addressManager.GetAddressState(amsHomeAddress.CountryCode, amsHomeAddress.State);
                homeAddress.ZipCode = amsHomeAddress.PostalCode;
                homeAddress.Country = addressManager.GetAddressCountry(amsHomeAddress.CountryCode);
                homeAddress.CongressionalDistrict = amsHomeAddress.CongressionalDistrict;

                addressManager.SetAddress(homeAddress);
            }
            else if (amsHomeAddress == null)
            {
                AddressManager addressManager = new AddressManager(_context);
                Address homeAddress = addressManager.GetHomeAddress(license);

                if (homeAddress != null && homeAddress.AmsAddressId > 0)
                {
                    addressManager.DeleteAddress(homeAddress);
                }
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

                agentOfServiceAddress.AmsAddressId = (int)amsAgentOfServiceAddress.AddressId;
                agentOfServiceAddress.Address1 = amsAgentOfServiceAddress.Address1;
                agentOfServiceAddress.Address2 = amsAgentOfServiceAddress.Address2;
                agentOfServiceAddress.City = amsAgentOfServiceAddress.City;
                agentOfServiceAddress.State = addressManager.GetAddressState(amsAgentOfServiceAddress.CountryCode, amsAgentOfServiceAddress.State);
                agentOfServiceAddress.ZipCode = amsAgentOfServiceAddress.PostalCode;
                agentOfServiceAddress.Country = addressManager.GetAddressCountry(amsAgentOfServiceAddress.CountryCode);
                agentOfServiceAddress.CongressionalDistrict = amsAgentOfServiceAddress.CongressionalDistrict;

                addressManager.SetAddress(agentOfServiceAddress);
            }
            else if (amsAgentOfServiceAddress == null)
            {
                AddressManager addressManager = new AddressManager(_context);
                Address agentOfServiceAddress = addressManager.GetAgentOfServiceAddress(license);

                if (agentOfServiceAddress != null && agentOfServiceAddress.AmsAddressId > 0)
                {
                    addressManager.DeleteAddress(agentOfServiceAddress);
                }
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
            if (amsPrimaryPhoneNumber != null && amsPrimaryPhoneNumber.PhoneCommLocationCode != WSBA.AMS.Enums.CommunicationLocationCode.NONE &&
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
            if (amsHomePhoneNumber != null && amsHomePhoneNumber.PhoneCommLocationCode != WSBA.AMS.Enums.CommunicationLocationCode.NONE &&
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

                homePhoneNumber.AmsLocationCode = WSBA.AMS.Enums.GetEnumDescription(amsHomePhoneNumber.PhoneCommLocationCode);
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
            if (amsFaxPhoneNumber != null && amsFaxPhoneNumber.PhoneCommLocationCode != WSBA.AMS.Enums.CommunicationLocationCode.NONE &&
                (license.LastAmsUpdate == null || 
                (license.LastAmsUpdate <= amsFaxPhoneNumber.AddedDate && amsFaxPhoneNumber.AddedDate < nextAmsUpdate) || 
                (license.LastAmsUpdate <= amsFaxPhoneNumber.ModifiedDate && amsFaxPhoneNumber.ModifiedDate < nextAmsUpdate)))
            {
                PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
                PhoneNumber faxPhoneNumber = phoneNumberManager.GetFaxPhoneNumber(license);

                if (faxPhoneNumber == null)
                {
                    faxPhoneNumber = new PhoneNumber()
                    {
                        LicenseId = license.LicenseId,
                        PhoneNumberType = phoneNumberManager.GetPhoneNumberType("Fax")
                    };
                }

                faxPhoneNumber.AmsLocationCode = WSBA.AMS.Enums.GetEnumDescription(amsFaxPhoneNumber.PhoneCommLocationCode);
                faxPhoneNumber.Country = phoneNumberManager.GetCountry(amsFaxPhoneNumber.PhoneCountryCode);
                faxPhoneNumber.AreaCode = amsFaxPhoneNumber.PhoneAreaCode;
                faxPhoneNumber.Number = amsFaxPhoneNumber.PhoneNumber;

                if (amsFaxPhoneNumber.PhoneExtension != null && amsFaxPhoneNumber.PhoneExtension.Length > 0)
                {
                    faxPhoneNumber.Extension = amsFaxPhoneNumber.PhoneExtension;
                }

                phoneNumberManager.SetPhoneNumber(faxPhoneNumber);
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
            else if (amsGender == null && license.Gender != null && license.LastAmsUpdate != null &&
                WSBA.AMS.DemographicManager.HasGenderRecentlyExpired(masterCustomerId, (DateTime)license.LastAmsUpdate))
            {
                GenderManager genderManager = new GenderManager(_context);
                genderManager.DeleteGender(license);
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
            else if (amsEthnicity == null && license.Ethnicity != null && license.LastAmsUpdate != null &&
                WSBA.AMS.DemographicManager.HasEthnicityRecentlyExpired(masterCustomerId, (DateTime)license.LastAmsUpdate))
            {
                EthnicityManager ethnicityManager = new EthnicityManager(_context);
                ethnicityManager.DeleteEthnicity(license);
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
            else if (amsSexualOrientation == null && license.SexualOrientation != null && license.LastAmsUpdate != null &&
                WSBA.AMS.DemographicManager.HasSexualOrientationRecentlyExpired(masterCustomerId, (DateTime)license.LastAmsUpdate))
            {
                SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);
                sexualOrientationManager.DeleteSexualOrientation(license);
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
            else if (amsDisability == null && license.Disability != null && license.LastAmsUpdate != null &&
                WSBA.AMS.DemographicManager.HasDisabilityRecentlyExpired(masterCustomerId, (DateTime)license.LastAmsUpdate))
            {
                DisabilityManager disabilityManager = new DisabilityManager(_context);
                disabilityManager.DeleteDisability(license);
            }
        }

        private void UpdateAreasOfPractice(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsAreasOfPractice = WSBA.AMS.DemographicManager.GetAreasOfPractice(masterCustomerId);

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
            else if (amsFirmSize == null && license.FirmSize != null && license.LastAmsUpdate != null &&
                WSBA.AMS.DemographicManager.HasFirmSizeRecentlyExpired(masterCustomerId, (DateTime)license.LastAmsUpdate))
            {
                FirmSizeManager firmSizeManager = new FirmSizeManager(_context);
                firmSizeManager.DeleteFirmSize(license);
            }
        }

        private void UpdateLanguages(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            var amsLanguages = WSBA.AMS.DemographicManager.GetLanguages(masterCustomerId);

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
            }
        }

        private void UpdateSections(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            SectionManager sectionManager = new SectionManager(_context);
            
            //get cycle end date
            DateTime cycleEndDate = new DateTime(license.LicensePeriod.EndDate.Year, 12, 31);

            var amsCanceledSectionOrders = WSBA.AMS.OrderManager.GetCanceledSectionOrderDetail(masterCustomerId, cycleEndDate);
            var amsActiveSectionOrders = WSBA.AMS.OrderManager.GetActiveSectionOrderDetail(masterCustomerId, cycleEndDate);
            var amsProformaSectionOrders = WSBA.AMS.OrderManager.GetProformaSectionOrderDetail(masterCustomerId, cycleEndDate);

            //delete canceled sections
            foreach (var amsCanceledSectionOrder in amsCanceledSectionOrders)
            {
                if (license.LastAmsUpdate == null ||
                    (license.LastAmsUpdate <= amsCanceledSectionOrder.AddedDate && amsCanceledSectionOrder.AddedDate < nextAmsUpdate) ||
                    (license.LastAmsUpdate <= amsCanceledSectionOrder.ModifiedDate && amsCanceledSectionOrder.ModifiedDate < nextAmsUpdate))
                {
                    var section = sectionManager.GetProduct(amsCanceledSectionOrder.ProductCode);
                    sectionManager.DeleteSection(license, section.SectionProductId);
                }
            }

            //set active sections
            foreach (var amsActiveSectionOrder in amsActiveSectionOrders)
            {
                if (license.LastAmsUpdate == null ||
                    (license.LastAmsUpdate <= amsActiveSectionOrder.AddedDate && amsActiveSectionOrder.AddedDate < nextAmsUpdate) ||
                    (license.LastAmsUpdate <= amsActiveSectionOrder.ModifiedDate && amsActiveSectionOrder.ModifiedDate < nextAmsUpdate))
                {
                    var section = sectionManager.GetProduct(amsActiveSectionOrder.ProductCode);
                    sectionManager.SetSection(license, section.SectionProductId);
                }
            }

            //set proforma sections
            foreach (var amsProformaSectionOrder in amsProformaSectionOrders)
            {
                if (license.LastAmsUpdate == null ||
                    (license.LastAmsUpdate <= amsProformaSectionOrder.AddedDate && amsProformaSectionOrder.AddedDate < nextAmsUpdate) ||
                    (license.LastAmsUpdate <= amsProformaSectionOrder.ModifiedDate && amsProformaSectionOrder.ModifiedDate < nextAmsUpdate))
                {
                    var section = sectionManager.GetProduct(amsProformaSectionOrder.ProductCode);
                    sectionManager.SetSection(license, section.SectionProductId);
                }
            }
        }

        private void UpdateLicenseFeeExemption(ref License license, string masterCustomerId, DateTime nextAmsUpdate)
        {
            //get cycle end date
            DateTime cycleEndDate = new DateTime(license.LicensePeriod.EndDate.Year, 12, 31);

            //load exemption orders
            var amsHardshipExemption = WSBA.AMS.OrderManager.GetHardshipExemption(masterCustomerId, cycleEndDate);
            var amsArmedForcesExemption = WSBA.AMS.OrderManager.GetArmedForcesExemption(masterCustomerId, cycleEndDate);

            bool hasHardshipExemption = false;
            bool hasArmedForcesExemption = false;

            //determine whether member has hardship exemption
            if (amsHardshipExemption.OrderNumber_OrderDetail != null && amsHardshipExemption.OrderNumber_OrderDetail != "")
            {
                if (license.LastAmsUpdate == null ||
                    (license.LastAmsUpdate <= amsHardshipExemption.AddedDate && amsHardshipExemption.AddedDate < nextAmsUpdate) ||
                    (license.LastAmsUpdate <= amsHardshipExemption.ModifiedDate && amsHardshipExemption.ModifiedDate < nextAmsUpdate))
                {
                    if (amsHardshipExemption.OrderLineStatus == WSBA.AMS.Enums.OrderLineStatusCode.Active)
                    {
                        hasHardshipExemption = true;
                    }
                }
            }

            //determine whether member has armed forces exemption
            if (amsArmedForcesExemption.OrderNumber_OrderDetail != null && amsArmedForcesExemption.OrderNumber_OrderDetail != "")
            {
                if (license.LastAmsUpdate == null ||
                    (license.LastAmsUpdate <= amsArmedForcesExemption.AddedDate && amsArmedForcesExemption.AddedDate < nextAmsUpdate) ||
                    (license.LastAmsUpdate <= amsArmedForcesExemption.ModifiedDate && amsArmedForcesExemption.ModifiedDate < nextAmsUpdate))
                {
                    if (amsArmedForcesExemption.OrderLineStatus == WSBA.AMS.Enums.OrderLineStatusCode.Active)
                    {
                        hasArmedForcesExemption = true;
                    }
                }
            }

            bool hasLicenseFeeExemption = hasHardshipExemption || hasArmedForcesExemption;
            MembershipProductManager membershipProductManager = new MembershipProductManager(_context);
            membershipProductManager.SetLicenseFeeExeption(license, hasLicenseFeeExemption);
        }

        private void UpdateLicenseOrder(ref License license, string masterCustomerId)
        {
            //get primary licensing product
            MembershipProductManager membershipProductManager = new MembershipProductManager(_context);
            PaymentManager paymentManager = new PaymentManager(_context);
            OrderManager orderManager = new OrderManager(_context);

            //get primary license product for license type
            var primaryLicenseProduct = membershipProductManager.GetPrimaryLicenseTypeProduct(license.LicenseType).Product;

            //get cycle end date
            DateTime cycleEndDate = new DateTime(license.LicensePeriod.EndDate.Year, 12, 31);

            //find paid order with right cycle date and primary license product
            var licenseOrder = WSBA.AMS.OrderManager.GetActiveOrderDetail(masterCustomerId, primaryLicenseProduct.AmsCode, cycleEndDate);

            //if no paid order, find proforma order with right cycle date and primary license product
            if (licenseOrder.OrderNumber_OrderDetail == null || licenseOrder.OrderNumber_OrderDetail == "")
            {
                licenseOrder = WSBA.AMS.OrderManager.GetProformaOrderDetail(masterCustomerId, primaryLicenseProduct.AmsCode, cycleEndDate);
            }

            if (licenseOrder.OrderNumber_OrderDetail != null && licenseOrder.OrderNumber_OrderDetail != "")
            {
                //get order number of primary license product
                int orderNumber = Convert.ToInt32(licenseOrder.OrderNumber_OrderDetail);

                //save order with order number
                orderManager.SetLicenseOrder(license, orderNumber);

                //get license type products
                var licenseTypeProducts = membershipProductManager.GetLicenseTypeProducts(license);

                //loop through license type products and update payment for each product
                if (licenseTypeProducts != null)
                {
                    foreach (var licenseTypeProduct in licenseTypeProducts)
                    {
                        var amsTransactions = WSBA.AMS.PaymentManager.GetTransactions(masterCustomerId, licenseTypeProduct.Product.AmsCode, orderNumber.ToString());

                        if (amsTransactions == null || amsTransactions.Count == 0)
                        {
                            paymentManager.DeleteTransactions(license.LicensingOrder, licenseTypeProduct.Product.AmsCode);
                        }
                        else
                        {
                            var transactionsToDelete =
                                (license.LicensingOrder.Transactions != null) ?
                                license.LicensingOrder.Transactions.Where(t => t.AmsCode == licenseTypeProduct.Product.AmsCode).Select(t => t.AmsTransactionId).ToList() :
                                null;

                            //loop through ams transactions
                            foreach (var amsTransaction in amsTransactions)
                            {
                                paymentManager.SetTransaction(license.LicensingOrder,
                                    licenseTypeProduct.Product.AmsCode, amsTransaction.TransactionNumber, amsTransaction.ActualAmount, amsTransaction.TransactionDate);

                                if (transactionsToDelete != null && transactionsToDelete.Contains(amsTransaction.TransactionNumber))
                                {
                                    transactionsToDelete.Remove(amsTransaction.TransactionNumber);
                                }
                            }

                            //delete transactions that don't exists in ams
                            if (transactionsToDelete != null)
                            {
                                foreach (var amsTransactionId in transactionsToDelete)
                                {
                                    paymentManager.DeleteTransaction(license.LicensingOrder, amsTransactionId);
                                }
                            }
                        }
                    }
                }

                //loop through license type products and update payment for each product
                if (license.Donations != null)
                {
                    foreach (var donation in license.Donations)
                    {
                        var amsTransactions = WSBA.AMS.PaymentManager.GetTransactions(masterCustomerId, donation.Product.AmsCode, orderNumber.ToString());

                        if (amsTransactions == null || amsTransactions.Count == 0)
                        {
                            paymentManager.DeleteTransactions(license.LicensingOrder, donation.Product.AmsCode);
                        }
                        else
                        {
                            var transactionsToDelete =
                                (license.LicensingOrder.Transactions != null) ?
                                license.LicensingOrder.Transactions.Where(t => t.AmsCode == donation.Product.AmsCode).Select(t => t.AmsTransactionId).ToList() :
                                null;

                            //loop through ams transactions
                            foreach (var amsTransaction in amsTransactions)
                            {
                                paymentManager.SetTransaction(license.LicensingOrder,
                                    donation.Product.AmsCode, amsTransaction.TransactionNumber, amsTransaction.ActualAmount, amsTransaction.TransactionDate);

                                if (transactionsToDelete != null && transactionsToDelete.Contains(amsTransaction.TransactionNumber))
                                {
                                    transactionsToDelete.Remove(amsTransaction.TransactionNumber);
                                }
                            }

                            //delete transactions that don't exists in ams
                            if (transactionsToDelete != null)
                            {
                                foreach (var amsTransactionId in transactionsToDelete)
                                {
                                    paymentManager.DeleteTransaction(license.LicensingOrder, amsTransactionId);
                                }
                            }
                        }
                    }
                }
            }
            else if (license.LicensingOrder != null)
            {
                orderManager.DeleteOrder(license.LicensingOrder);
            }
        }

        private void UpdateSectionOrder(ref License license, string masterCustomerId)
        {
            //get primary licensing product
            PaymentManager paymentManager = new PaymentManager(_context);
            OrderManager orderManager = new OrderManager(_context);

            //get cycle end date
            DateTime cycleEndDate = new DateTime(license.LicensePeriod.EndDate.Year, 12, 31);

            //find paid order with right cycle date and section products
            var sectionOrders = WSBA.AMS.OrderManager.GetActiveSectionOrderDetail(masterCustomerId, cycleEndDate);

            //if no paid order, find proforma order with right cycle date and section products
            if (sectionOrders == null || sectionOrders.Count() == 0)
            {
                sectionOrders = WSBA.AMS.OrderManager.GetProformaSectionOrderDetail(masterCustomerId, cycleEndDate);
            }

            if (sectionOrders != null && sectionOrders.Count() > 0)
            {
                var sectionOrder = sectionOrders.FirstOrDefault();
                
                //get order number of primary license product
                int orderNumber = Convert.ToInt32(sectionOrder.OrderNumber_OrderDetail);

                //save order with order number
                orderManager.SetSectionOrder(license, orderNumber);

                //loop through sections and update payment for each section product
                if (license.Sections != null)
                {
                    foreach (var section in license.Sections)
                    {
                        var amsTransactions = WSBA.AMS.PaymentManager.GetTransactions(masterCustomerId, section.Product.AmsCode, orderNumber.ToString());

                        if (amsTransactions == null || amsTransactions.Count == 0)
                        {
                            paymentManager.DeleteTransactions(license.SectionOrder, section.Product.AmsCode);
                        }
                        else
                        {
                            var transactionsToDelete =
                                (license.SectionOrder.Transactions != null) ?
                                license.SectionOrder.Transactions.Where(t => t.AmsCode == section.Product.AmsCode).Select(t => t.AmsTransactionId).ToList() :
                                null;

                            //loop through ams transactions
                            foreach (var amsTransaction in amsTransactions)
                            {
                                paymentManager.SetTransaction(license.SectionOrder, 
                                    section.Product.AmsCode, amsTransaction.TransactionNumber, amsTransaction.ActualAmount, amsTransaction.TransactionDate);

                                if (transactionsToDelete != null && transactionsToDelete.Contains(amsTransaction.TransactionNumber))
                                {
                                    transactionsToDelete.Remove(amsTransaction.TransactionNumber);
                                }
                            }

                            //delete transactions that don't exists in ams
                            if (transactionsToDelete != null)
                            {
                                foreach (var amsTransactionId in transactionsToDelete)
                                {
                                    paymentManager.DeleteTransaction(license.SectionOrder, amsTransactionId);
                                }
                            }
                        }
                    }
                }
            }
            else if (license.SectionOrder != null)
            {
                orderManager.DeleteOrder(license.SectionOrder);
            }
        }
    }
}
