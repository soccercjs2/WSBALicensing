namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Licensing.Domain.Addresses;
    using Licensing.Domain.AreasOfPractice;
    using Licensing.Domain.ContactInformation;
    using Licensing.Domain.Disabilities;
    using Licensing.Domain.Donations;
    using Licensing.Domain.Ethnicities;
    using Licensing.Domain.FinancialResponsibilities;
    using Licensing.Domain.FirmSizes;
    using Licensing.Domain.Genders;
    using Licensing.Domain.Judicial;
    using Licensing.Domain.Languages;
    using Licensing.Domain.ProBonos;
    using Licensing.Domain.ProfessionalLiabilityInsurances;
    using Licensing.Domain.Sections;
    using Licensing.Domain.SexualOrientations;
    using Licensing.Domain.TrustAccounts;
    using System.Collections.Generic;
    using Domain.Licenses;
    using Domain.Enums;
    using Domain.Customers;
    using Domain.BarNews;
    using Domain.PracticeAreas;

    internal sealed class Configuration : DbMigrationsConfiguration<Licensing.Data.Context.LicensingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Licensing.Data.Context.LicensingContext context)
        {
            GenerateSeedData(context);
        }

        private void GenerateSeedData(Context.LicensingContext context)
        {
            //license intitializers
            var licensingPeriods = new List<LicensePeriod>
            {
                new LicensePeriod { StartDate = new DateTime(2017, 10, 16), EndDate = new DateTime(2018, 5, 1), LateFeeDate = new DateTime(2018, 2, 1) },
                new LicensePeriod { StartDate = new DateTime(2018, 10, 16), EndDate = new DateTime(2019, 5, 1), LateFeeDate = new DateTime(2019, 2, 1) }
            };

            var licensingProducts = new List<LicenseProduct>
            {
                new LicenseProduct { Name = "Active Attorney Membership", Price = 385, AmsCode = "ACTIVE_ATTRNY" },
                new LicenseProduct { Name = "Inactive Attorney Membership", Price = 200, AmsCode = "INACTIVE_ATTORNEY" },
                new LicenseProduct { Name = "Lawyers Fund for Client Protection", Price = 30, AmsCode = "LFCP" }
            };

            var licenseTypeProducts = new List<LicenseTypeProduct>
            {
                new LicenseTypeProduct { LicenseTypeId = 1, Product = licensingProducts[0] },
                new LicenseTypeProduct { LicenseTypeId = 1, Product = licensingProducts[2] },
                new LicenseTypeProduct { LicenseTypeId = 2, Product = licensingProducts[1] }
            };

            var licenseTypes = new List<LicenseType>
            {
                new LicenseType {
                    Name = "Active Attorney",
                    LicenseTypeProducts = new List<LicenseTypeProduct> { licenseTypeProducts[0], licenseTypeProducts[1] },
                    MembershipType = RequirementType.Optional,
                    JudicialPosition = RequirementType.Excluded,
                    PracticeAreas = RequirementType.Excluded,
                    TrustAccount = RequirementType.Required,
                    ProfessionalLiabilityInsurance = RequirementType.Required,
                    FinancialResponsibility = RequirementType.Excluded,
                    ProBono = RequirementType.Optional,
                    PrimaryAddress = RequirementType.Optional,
                    HomeAddress = RequirementType.Optional,
                    AgentOfServiceAddress = RequirementType.Optional,
                    PrimaryEmail = RequirementType.Optional,
                    PrimaryPhoneNumber = RequirementType.Optional,
                    HomePhoneNumber = RequirementType.Optional,
                    FaxPhoneNumber = RequirementType.Optional,
                    AreasOfPractice = RequirementType.Optional,
                    FirmSize = RequirementType.Optional,
                    Languages = RequirementType.Optional,
                    Disability = RequirementType.Optional,
                    Ethnicity = RequirementType.Optional,
                    Gender = RequirementType.Optional,
                    SexualOrientation = RequirementType.Optional,
                    Donations = RequirementType.Required,
                    Sections = RequirementType.Required,
                    BarNews = RequirementType.Excluded
                },
                new LicenseType {
                    Name = "Inactive Attorney",
                    LicenseTypeProducts = new List<LicenseTypeProduct> { licenseTypeProducts[2] },
                    MembershipType = RequirementType.Optional,
                    JudicialPosition = RequirementType.Excluded,
                    PracticeAreas = RequirementType.Excluded,
                    TrustAccount = RequirementType.Excluded,
                    ProfessionalLiabilityInsurance = RequirementType.Excluded,
                    FinancialResponsibility = RequirementType.Excluded,
                    ProBono = RequirementType.Optional,
                    PrimaryAddress = RequirementType.Optional,
                    HomeAddress = RequirementType.Optional,
                    AgentOfServiceAddress = RequirementType.Required,
                    PrimaryEmail = RequirementType.Optional,
                    PrimaryPhoneNumber = RequirementType.Optional,
                    HomePhoneNumber = RequirementType.Optional,
                    FaxPhoneNumber = RequirementType.Optional,
                    AreasOfPractice = RequirementType.Optional,
                    FirmSize = RequirementType.Optional,
                    Languages = RequirementType.Optional,
                    Disability = RequirementType.Optional,
                    Ethnicity = RequirementType.Optional,
                    Gender = RequirementType.Optional,
                    SexualOrientation = RequirementType.Optional,
                    Donations = RequirementType.Required,
                    Sections = RequirementType.Required,
                    BarNews = RequirementType.Optional
                }
            };

            //licensing information initializers
            var coveredByOptions = new List<CoveredByOption>
            {
                new CoveredByOption { Name = "Company", AmsCode = "COMPANY" },
                new CoveredByOption { Name = "Policy", AmsCode = "POLICY" }
            };

            var financialResponsibilities = new List<FinancialResponsibility>
            {
                new FinancialResponsibility
                {
                    CoveredByOption = coveredByOptions[0],
                    Company = "WSBA",
                    PolicyNumber = "ABC123"
                }
            };

            var judicialPositionOptions = new List<JudicialPositionOption>
            {
                new JudicialPositionOption { Name = "US Bankruptcy Court", AmsCode = "US_BANKRUPTCY_COURT" },
                new JudicialPositionOption { Name = "US District Court", AmsCode = "US_DISTRICT_COURT" },
                new JudicialPositionOption { Name = "US Supreme Court", AmsCode = "US_SUPREME_COURT" },
                new JudicialPositionOption { Name = "WA Court of Appeals", AmsCode = "WA_COURT_OF_APPEALS" },
                new JudicialPositionOption { Name = "WA Superior Court", AmsCode = "WA_SUPERIOR_COURT" },
                new JudicialPositionOption { Name = "WA Supreme Court", AmsCode = "WA_SUPREME_COURT" },
                new JudicialPositionOption { Name = "Tribal Law", AmsCode = "WA_TRIBAL_COURT" },
                new JudicialPositionOption { Name = "United States Administrative Law", CitationRequired = true, AmsCode = "US_ADMINISTRATIVE_LAW" }
            };

            var judicialPositions = new List<JudicialPosition>
            {
                new JudicialPosition { Option = judicialPositionOptions[0] }
            };

            var practiceAreaOptions = new List<PracticeAreaOption>
            {
                new PracticeAreaOption { Name = "Business Law", AmsCode = "BUSINESS_LAW" },
                new PracticeAreaOption { Name = "Criminal Law", AmsCode = "CRIMINAL_LAW" },
                new PracticeAreaOption { Name = "Family Law", AmsCode = "FAMILY_LAW" }
            };

            var practiceAreas = new List<PracticeArea>
            {
                new PracticeArea { Option = practiceAreaOptions[0] },
                new PracticeArea { Option = practiceAreaOptions[1] },
                new PracticeArea { Option = practiceAreaOptions[2] }
            };

            var professionalLiabilityInsuranceOption = new List<ProfessionalLiabilityInsuranceOption>
            {
                new ProfessionalLiabilityInsuranceOption { Description = "Engaged in the private practice of law, covered by, and intend to maintain Professional Liability Insurance." },
                new ProfessionalLiabilityInsuranceOption { Description = "Engaged in the private practice of law, covered by, but DO NOT intend to maintain, Professional Liability Insurance." },
                new ProfessionalLiabilityInsuranceOption { Description = "Engaged in the private practice of law BUT NOT covered by Professional Liability Insurance." },
                new ProfessionalLiabilityInsuranceOption { Description = "NOT engaged in the private practice of law because: (1) I do not practice law, or (2) I practice law as a government lawyer, or (3) I am employed by an organizational client, and I do not represent clients outside that capacity." }
            };

            var professionalLiabilityInsurances = new List<ProfessionalLiabilityInsurance>
            {
                new ProfessionalLiabilityInsurance { Option = professionalLiabilityInsuranceOption[0] }
            };

            var trustAccountNumbers = new List<TrustAccountNumber>
            {
                new TrustAccountNumber { TrustAccountId = 1, Bank = "BOA", Branch = "Seattle", AccountNumber = "12345678" },
                new TrustAccountNumber { TrustAccountId = 1, Bank = "BECU", Branch = "Tacoma", AccountNumber = "23456789" }
            };

            var trustAccounts = new List<TrustAccount>
            {
                new TrustAccount { HandlesTrustAccount = true, TrustAccountNumbers = trustAccountNumbers }
            };

            //contact information initializers
            var addressTypes = new List<AddressType>
            {
                new AddressType { Name = "Primary", AmsCode = "OFFICE" },
                new AddressType { Name = "Home", AmsCode = "HOME" },
                new AddressType { Name = "Agent of Service", AmsCode = "AGENTOFSERVICE" }
            };

            var addresses = new List<Address>
            {
                new Address { AddressType = addressTypes[0], Address1 = "1325 4th Ave NE", City = "Seattle", State = "WA", ZipCode = "98101", Country = "USA", CongressionalDistrict = "0" },
                new Address { AddressType = addressTypes[1], Address1 = "1325 5th Ave NE", City = "Seattle", State = "WA", ZipCode = "98101", Country = "USA", CongressionalDistrict = "0" },
                new Address { AddressType = addressTypes[2], Address1 = "1325 6th Ave NE", City = "Seattle", State = "WA", ZipCode = "98101", Country = "USA", CongressionalDistrict = "0" }
            };

            var emails = new List<Email>
            {
                new Email { EmailAddress = "collins@wsba.org" },
                new Email { EmailAddress = "soccercjs2@gmail.com" }
            };

            var phoneNumberTypes = new List<PhoneNumberType>
            {
                new PhoneNumberType { Name = "Primary", AmsCode = "OFFICE" },
                new PhoneNumberType { Name = "Home", AmsCode = "HOME" },
                new PhoneNumberType { Name = "Fax", AmsCode = "FAX" }
            };

            var phoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber { PhoneNumberType = phoneNumberTypes[0], CountryCode = 1, Number = "(555) 123-4567", Extension = "999" },
                new PhoneNumber { PhoneNumberType = phoneNumberTypes[1], CountryCode = 1, Number = "(555) 123-4568" },
                new PhoneNumber { PhoneNumberType = phoneNumberTypes[2], CountryCode = 1, Number = "(555) 123-4569" }
            };

            //practice information initializers
            var areaOfPracticeOptions = new List<AreaOfPracticeOption>
            {
                new AreaOfPracticeOption { Name = "Business Law", AmsCode = "BUSINESS_LAW" },
                new AreaOfPracticeOption { Name = "Criminal Law", AmsCode = "CRIMINAL_LAW" },
                new AreaOfPracticeOption { Name = "Family Law", AmsCode = "FAMILY_LAW" }
            };

            var areasOfPractice = new List<AreaOfPractice>
            {
                new AreaOfPractice { Option = areaOfPracticeOptions[0] },
                new AreaOfPractice { Option = areaOfPracticeOptions[1] },
                new AreaOfPractice { Option = areaOfPracticeOptions[2] }
            };

            var firmSizeOptions = new List<FirmSizeOption>
            {
                new FirmSizeOption { Name = "Solo", AmsCode = "SOLO" },
                new FirmSizeOption { Name = "Government", AmsCode = "GOVERNMENT" }
            };

            var firmSizes = new List<FirmSize>
            {
                new FirmSize { Option = firmSizeOptions[0] }
            };

            var languageOptions = new List<LanguageOption>
            {
                new LanguageOption { Name = "English", AmsCode = "ENGLISH" },
                new LanguageOption { Name = "Spanish", AmsCode = "SPANISH" }
            };

            var languages = new List<Language>
            {
                new Language { Option = languageOptions[0] }
            };

            //demographics initializers
            var disabilityOptions = new List<DisabilityOption>
            {
                new DisabilityOption { Name = "Yes", AmsCode = "YES" },
                new DisabilityOption { Name = "No", AmsCode = "NO" }
            };

            var disabilities = new List<Disability>
            {
                new Disability { Option = disabilityOptions[0] }
            };

            var ethnicityOptions = new List<EthnicityOption>
            {
                new EthnicityOption { Name = "Black", AmsCode = "BLACK" },
                new EthnicityOption { Name = "White", AmsCode = "CAUC" }
            };

            var ethnicities = new List<Ethnicity>
            {
                new Ethnicity { Option = ethnicityOptions[0] }
            };

            var genderOptions = new List<GenderOption>
            {
                new GenderOption { Name = "Male", AmsCode = "MALE" },
                new GenderOption { Name = "Female", AmsCode = "FEMALE" }
            };

            var genders = new List<Gender>
            {
                new Gender { Option = genderOptions[0] }
            };

            var sexualOrientationOptions = new List<SexualOrientationOption>
            {
                new SexualOrientationOption { Name = "Yes", AmsCode = "YES" },
                new SexualOrientationOption { Name = "No", AmsCode = "NO" }
            };

            var sexualOrientations = new List<SexualOrientation>
            {
                new SexualOrientation { Option = sexualOrientationOptions[0] }
            };

            //payment initializers
            var donationProducts = new List<DonationProduct>
            {
                new DonationProduct { Name = "Bar Foundation Donation", AmsCode = "BFD" },
                new DonationProduct { Name = "Campaign For Equal Justice Donation", AmsCode = "C4EJ" }
            };

            var donations = new List<Donation>
            {
                new Donation { Product = donationProducts[0], Amount = 50 },
                new Donation { Product = donationProducts[1], Amount = 50 },
                new Donation { Product = donationProducts[0], Amount = 50 },
                new Donation { Product = donationProducts[1], Amount = 50 }
            };

            var sectionProducts = new List<SectionProduct>
            {
                new SectionProduct { Name = "Business Law", Price = 25, AmsCode = "BUSINESS_LAW" },
                new SectionProduct { Name = "Family Law", Price = 35, AmsCode = "FAMILY_LAW" }
            };

            var sections = new List<Section>
            {
                new Section { Product = sectionProducts[0] },
                new Section { Product = sectionProducts[1] }
            };

            var barNewses = new List<BarNewsResponse>
            {
                new BarNewsResponse { Response = true }
            };

            var licenses = new List<License>
            {
                new License
                {
                    LicensePeriod = licensingPeriods[1],
                    LicenseType = licenseTypes[0],
                    PreviousLicenseType = licenseTypes[0],
                    FinancialResponsibility = financialResponsibilities[0],
                    JudicialPosition = judicialPositions[0],
                    PracticeAreas = practiceAreas,
                    ProfessionalLiabilityInsurance = professionalLiabilityInsurances[0],
                    TrustAccount = trustAccounts[0],
                    Addresses = addresses,
                    Email = emails[0],
                    PhoneNumbers = phoneNumbers,
                    AreasOfPractice = areasOfPractice,
                    FirmSize = firmSizes[0],
                    Languages = languages,
                    Donations = new List<Donation> { donations[0], donations[1] },
                    Sections = sections,
                    BarNewsResponse = barNewses[0]
                },
                new License
                {
                    LicensePeriod = licensingPeriods[0],
                    LicenseType = licenseTypes[0],
                    PreviousLicenseType = licenseTypes[0],
                    FinancialResponsibility = null,
                    JudicialPosition = null,
                    PracticeAreas = null,
                    ProBono = null,
                    ProfessionalLiabilityInsurance = null,
                    TrustAccount = null,
                    Addresses = null,
                    Email = null,
                    PhoneNumbers = null,
                    AreasOfPractice = null,
                    FirmSize = null,
                    Languages = null,
                    Disability = null,
                    Ethnicity = null,
                    Gender = null,
                    SexualOrientation = null,
                    Donations = new List<Donation> { donations[2], donations[3] },
                    Sections = null,
                    BarNewsResponse = null
                }
            };

            var customers = new List<Customer>
            {
                new Customer {
                    BarNumber = "555",
                    FirstName = "Charles",
                    LastName = "Brown",
                    Licenses = licenses
                }
            };

            licensingPeriods.ForEach(x => context.LicensePeriods.Add(x));
            licensingProducts.ForEach(x => context.LicenseProducts.Add(x));
            licenseTypeProducts.ForEach(x => context.LicenseTypeProducts.Add(x));
            licenseTypes.ForEach(x => context.LicenseTypes.Add(x));
            judicialPositionOptions.ForEach(x => context.JudicialPositionOptions.Add(x));
            judicialPositions.ForEach(x => context.JudicialPositions.Add(x));
            practiceAreaOptions.ForEach(x => context.PracticeAreaOptions.Add(x));
            practiceAreas.ForEach(x => context.PracticeAreas.Add(x));
            coveredByOptions.ForEach(x => context.CoveredByOptions.Add(x));
            financialResponsibilities.ForEach(x => context.FinancialResponsibilities.Add(x));
            //proBono.ForEach(x => context.ProBonos.Add(x));
            professionalLiabilityInsuranceOption.ForEach(x => context.ProfessionalLiabilityInsuranceOptions.Add(x));
            professionalLiabilityInsurances.ForEach(x => context.ProfessionalLiabilityInsurances.Add(x));
            trustAccountNumbers.ForEach(x => context.TrustAccountNumbers.Add(x));
            trustAccounts.ForEach(x => context.TrustAccounts.Add(x));
            addressTypes.ForEach(x => context.AddressTypes.Add(x));
            addresses.ForEach(x => context.Addresses.Add(x));
            emails.ForEach(x => context.Emails.Add(x));
            phoneNumberTypes.ForEach(x => context.PhoneNumberTypes.Add(x));
            phoneNumbers.ForEach(x => context.PhoneNumbers.Add(x));
            areaOfPracticeOptions.ForEach(x => context.AreaOfPracticeOptions.Add(x));
            areasOfPractice.ForEach(x => context.AreasOfPractice.Add(x));
            firmSizeOptions.ForEach(x => context.FirmSizeOptions.Add(x));
            firmSizes.ForEach(x => context.FirmSizes.Add(x));
            languageOptions.ForEach(x => context.LanguageOptions.Add(x));
            languages.ForEach(x => context.Languages.Add(x));
            disabilityOptions.ForEach(x => context.DisabilityOptions.Add(x));
            disabilities.ForEach(x => context.Disabilities.Add(x));
            ethnicityOptions.ForEach(x => context.EthnicityOptions.Add(x));
            ethnicities.ForEach(x => context.Ethnicities.Add(x));
            genderOptions.ForEach(x => context.GenderOptions.Add(x));
            genders.ForEach(x => context.Genders.Add(x));
            sexualOrientationOptions.ForEach(x => context.SexualOrientationOptions.Add(x));
            sexualOrientations.ForEach(x => context.SexualOrientations.Add(x));
            donationProducts.ForEach(x => context.DonationProducts.Add(x));
            donations.ForEach(x => context.Donations.Add(x));
            sectionProducts.ForEach(x => context.SectionProducts.Add(x));
            sections.ForEach(x => context.Sections.Add(x));
            licenses.ForEach(x => context.Licenses.Add(x));
            customers.ForEach(x => context.Customers.Add(x));

            context.SaveChanges();
        }
    }
}
