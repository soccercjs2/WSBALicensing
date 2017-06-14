using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using Licensing.Data.Initializer;

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
using Licensing.Domain.BarNews;
using Licensing.Domain.Judicial;
using Licensing.Domain.PracticeAreas;
using Licensing.Domain.Transactions;
using Licensing.Domain.Orders;
using Licensing.Domain.Employers;
using Licensing.Domain.Keller;
using Licensing.Domain.Hardship;

namespace Licensing.Data.Context
{
    public class LicensingContext : DbContext
    {
        public LicensingContext() : base("LicensingContext")
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            Database.SetInitializer(new LicensingInitializer());
            if (!Database.Exists())
            {
                Database.Initialize(true);
            }
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<AddressCountry> AddressCountries { get; set; }
        public DbSet<AddressState> AddressStates { get; set; }
        public DbSet<AreaOfPractice> AreasOfPractice { get; set; }
        public DbSet<AreaOfPracticeOption> AreaOfPracticeOptions { get; set; }
        public DbSet<BarNewsResponse> BarNewsResponses { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public DbSet<PhoneNumberCountry> PhoneNumberCountries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<DisabilityOption> DisabilityOptions { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonationProduct> DonationProducts { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<EthnicityOption> EthnicityOptions { get; set; }
        public DbSet<FinancialResponsibility> FinancialResponsibilities { get; set; }
        public DbSet<CoveredByOption> CoveredByOptions { get; set; }
        public DbSet<FirmSize> FirmSizes { get; set; }
        public DbSet<FirmSizeOption> FirmSizeOptions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<GenderOption> GenderOptions { get; set; }
        public DbSet<HardshipExemptionRequest> HardshipExemptionRequests { get; set; }
        public DbSet<JudicialPosition> JudicialPositions { get; set; }
        public DbSet<JudicialPositionOption> JudicialPositionOptions { get; set; }
        public DbSet<KellerDiscount> KellerDiscounts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageOption> LanguageOptions { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<LicenseTypeRequirement> LicenseTypeRequirements { get; set; }
        public DbSet<LicenseTypeDonation> LicenseTypeDonations { get; set; }
        public DbSet<LicenseTypeProduct> LicenseTypeProducts { get; set; }
        public DbSet<LicenseTypeSection> LicenseTypeSections { get; set; }
        public DbSet<LicensePeriod> LicensePeriods { get; set; }
        public DbSet<LicenseProduct> LicenseProducts { get; set; }
        public DbSet<LicenseProductPrice> LicenseProductPrices { get; set; }
        public DbSet<PracticeArea> PracticeAreas { get; set; }
        public DbSet<PracticeAreaOption> PracticeAreaOptions { get; set; }
        public DbSet<ProBono> ProBonos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProfessionalLiabilityInsurance> ProfessionalLiabilityInsurances { get; set; }
        public DbSet<ProfessionalLiabilityInsuranceOption> ProfessionalLiabilityInsuranceOptions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionProduct> SectionProducts { get; set; }
        public DbSet<SexualOrientation> SexualOrientations { get; set; }
        public DbSet<SexualOrientationOption> SexualOrientationOptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TrustAccount> TrustAccounts { get; set; }
        public DbSet<TrustAccountNumber> TrustAccountNumbers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<KellerDiscount>().Property(x => x.DiscountPercentage).HasPrecision(18, 4);
        }
    }
}
