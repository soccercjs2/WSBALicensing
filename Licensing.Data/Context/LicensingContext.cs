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
        public DbSet<AreaOfPractice> AreasOfPractice { get; set; }
        public DbSet<AreaOfPracticeOption> AreaOfPracticeOptions { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<DisabilityOption> DisabilityOptions { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonationProduct> DonationProducts { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<EthnicityOption> EthnicityOptions { get; set; }
        public DbSet<FinancialResponsibility> FinancialResponsibilities { get; set; }
        public DbSet<CoveredByOption> CoveredByOptions { get; set; }
        public DbSet<FirmSize> FirmSizes { get; set; }
        public DbSet<FirmSizeOption> FirmSizeOptions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<GenderOption> GenderOptions { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageOption> LanguageOptions { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<LicenseTypeProduct> LicenseTypeProducts { get; set; }
        public DbSet<LicensingPeriod> LicensingPeriods { get; set; }
        public DbSet<LicensingProduct> LicensingProducts { get; set; }
        public DbSet<ProBono> ProBonos { get; set; }
        public DbSet<ProfessionalLiabilityInsurance> ProfessionalLiabilityInsurances { get; set; }
        public DbSet<ProfessionalLiabilityInsuranceOption> ProfessionalLiabilityInsuranceOptions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionProduct> SectionProducts { get; set; }
        public DbSet<SexualOrientation> SexualOrientations { get; set; }
        public DbSet<SexualOrientationOption> SexualOrientationOptions { get; set; }
        public DbSet<TrustAccount> TrustAccounts { get; set; }
        public DbSet<TrustAccountNumber> TrustAccountNumbers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
