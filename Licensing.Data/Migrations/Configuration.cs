namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Licensing.Domain.Addresses;
    using Licensing.Domain.AreasOfPractice;
    using Licensing.Domain.ContactInformation;
    using Licensing.Domain.Customers;
    using Licensing.Domain.Donations;
    using Licensing.Domain.FinancialResponsibilities;
    using Licensing.Domain.FirmSizes;
    using Licensing.Domain.Languages;
    using Licensing.Domain.Licenses;
    using Licensing.Domain.ProBonos;
    using Licensing.Domain.ProfessionalLiabilityInsurances;
    using Licensing.Domain.Sections;
    using Licensing.Domain.TrustAccounts;
    using Licensing.Domain.Disabilities;
    using Licensing.Domain.Ethnicities;
    using Licensing.Domain.Genders;
    using Licensing.Domain.SexualOrientations;
    using System.Collections.Generic;

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
            var licensingPeriods = new List<LicensingPeriod>
            {
                new LicensingPeriod
                {
                    StartDate = new DateTime(2017, 10, 16),
                    EndDate = new DateTime(2018, 5, 1),
                    LateFeeDate = new DateTime(2018, 2, 1)
                }
            };

            var licensingProducts = new List<LicensingProduct>
            {
                new LicensingProduct { Name = "Active Attorney Membership", Price = 385, AmsCode = "ACTIVE_ATTRNY" },
                new LicensingProduct { Name = "Lawyers Fund for Client Protection", Price = 30, AmsCode = "LFCP" }
            };

            var licenseTypeProducts = new List<LicenseTypeProduct>
            {
                new LicenseTypeProduct { LicenseTypeId = 1, Product = licensingProducts[0] },
                new LicenseTypeProduct { LicenseTypeId = 1, Product = licensingProducts[1] }
            };

            var licenseTypes = new List<LicenseType>
            {
                new LicenseType { Name = "Active Attorney", LicenseTypeProducts = licenseTypeProducts }
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

            var proBono = new List<ProBono>
            {
                new ProBono
                {
                    ProvidesService = true,
                    FreeServiceHours = 5,
                    LimitedFeeServiceHours = 3,
                    Anonymous = false
                }
            };

            var professionalLiabilityInsuranceOption = new List<ProfessionalLiabilityInsuranceOption>
            {
                new ProfessionalLiabilityInsuranceOption { Description = "Option 1" },
                new ProfessionalLiabilityInsuranceOption { Description = "Option 2" }
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
                new AddressType { Name = "Agent Of Service", AmsCode = "AGENTOFSERVICE" }
            };

            var addresses = new List<Address>
            {
                new Address { LicenseId = 1, AddressType = addressTypes[0], Address1 = "1325 4th Ave NE", City = "Seattle", State = "WA", ZipCode = "98101", Country = "USA" },
                new Address { LicenseId = 1, AddressType = addressTypes[1], Address1 = "1325 5th Ave NE", City = "Seattle", State = "WA", ZipCode = "98101", Country = "USA" },
                new Address { LicenseId = 1, AddressType = addressTypes[2], Address1 = "1325 6th Ave NE", City = "Seattle", State = "WA", ZipCode = "98101", Country = "USA" }
            };

            var emailTypes = new List<EmailType>
            {
                new EmailType { Name = "Primary", AmsCode = "OFFICE" },
                new EmailType { Name = "Home", AmsCode = "HOME" }
            };

            var emails = new List<Email>
            {
                new Email { EmailAddress = "collins@wsba.org", EmailType = emailTypes[0] }
            };

            var phoneNumberTypes = new List<PhoneNumberType>
            {
                new PhoneNumberType { Name = "Primary", AmsCode = "OFFICE" },
                new PhoneNumberType { Name = "Home", AmsCode = "HOME" }
            };

            var phoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber { PhoneNumberType = phoneNumberTypes[0], CountryCode = 1, AreaCode = 555, Number = 1234567 },
                new PhoneNumber { PhoneNumberType = phoneNumberTypes[1], CountryCode = 1, AreaCode = 555, Number = 1234568 }
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
                new AreaOfPractice { LicenseId = 1, Option = areaOfPracticeOptions[0] },
                new AreaOfPractice { LicenseId = 1, Option = areaOfPracticeOptions[1] },
                new AreaOfPractice { LicenseId = 1, Option = areaOfPracticeOptions[2] }
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
                new Language { LicenseId = 1, Option = languageOptions[0] }
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
                new GenderOption { Name = "Black", AmsCode = "BLACK" },
                new GenderOption { Name = "White", AmsCode = "CAUC" }
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
                new DonationProduct { Name = "Bar Foundation Donation", Price = 50, AmsCode = "BFD" },
                new DonationProduct { Name = "Campaign For Equal Justice Donation", Price = 50, AmsCode = "C4EJ" }
            };
            var donations = new List<Donation>
            {
                new Donation { LicenseId = 1, Product = donationProducts[0] },
                new Donation { LicenseId = 1, Product = donationProducts[1] }
            };

            var sectionProducts = new List<SectionProduct>
            {
                new SectionProduct { Name = "Business Law", Price = 25, AmsCode = "BUSINESS_LAW" },
                new SectionProduct { Name = "Family Law", Price = 35, AmsCode = "FAMILY_LAW" }
            };

            var sections = new List<Section>
            {
                new Section { LicenseId = 1, Product = sectionProducts[0] },
                new Section { LicenseId = 1, Product = sectionProducts[1] }
            };

            var licenses = new List<License>
            {
                new License
                {
                    LicensingPeriod = licensingPeriods[0],
                    LicenseType = licenseTypes[0],
                    FinancialResponsibility = financialResponsibilities[0],
                    ProBono = proBono[0],
                    ProfessionalLiabilityInsurance = professionalLiabilityInsurances[0],
                    TrustAccount = trustAccounts[0],
                    Addresses = addresses,
                    Emails = emails,
                    PhoneNumbers = phoneNumbers,
                    AreasOfPractice = areasOfPractice,
                    FirmSize = firmSizes[0],
                    Languages = languages,
                    Disability = disabilities[0],
                    Ethnicity = ethnicities[0],
                    Gender = genders[0],
                    SexualOrientation = sexualOrientations[0],
                    Donations = donations,
                    Sections = sections,
                    BarNews = true
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

            licensingPeriods.ForEach(x => context.LicensingPeriods.Add(x));
            licensingProducts.ForEach(x => context.LicensingProducts.Add(x));
            licenseTypeProducts.ForEach(x => context.LicenseTypeProducts.Add(x));
            licenseTypes.ForEach(x => context.LicenseTypes.Add(x));
            coveredByOptions.ForEach(x => context.CoveredByOptions.Add(x));
            financialResponsibilities.ForEach(x => context.FinancialResponsibilities.Add(x));
            proBono.ForEach(x => context.ProBonos.Add(x));
            professionalLiabilityInsuranceOption.ForEach(x => context.ProfessionalLiabilityInsuranceOptions.Add(x));
            professionalLiabilityInsurances.ForEach(x => context.ProfessionalLiabilityInsurances.Add(x));
            trustAccountNumbers.ForEach(x => context.TrustAccountNumbers.Add(x));
            trustAccounts.ForEach(x => context.TrustAccounts.Add(x));
            addressTypes.ForEach(x => context.AddressTypes.Add(x));
            addresses.ForEach(x => context.Addresses.Add(x));
            emailTypes.ForEach(x => context.EmailTypes.Add(x));
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
