namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comeon : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AreaOfPractice", "AreaOfPracticeOptionId", "dbo.AreaOfPracticeOption");
            DropForeignKey("dbo.License", "LicenseTypeId", "dbo.LicenseType");
            DropForeignKey("dbo.License", "LicensingPeriodId", "dbo.LicensingPeriod");
            DropForeignKey("dbo.Donation", "DonationProductId", "dbo.DonationProduct");
            DropForeignKey("dbo.FinancialResponsibility", "CoveredByOptionId", "dbo.CoveredByOption");
            DropForeignKey("dbo.FirmSize", "FirmSizeOptionId", "dbo.FirmSizeOption");
            DropForeignKey("dbo.JudicialPosition", "JudicialPositionOptionId", "dbo.JudicialPositionOption");
            DropForeignKey("dbo.Language", "LanguageOptionId", "dbo.LanguageOption");
            DropForeignKey("dbo.LicenseTypeProduct", "LicensingProductId", "dbo.LicensingProduct");
            DropForeignKey("dbo.LicenseTypeSection", "SectionProductId", "dbo.SectionProduct");
            DropForeignKey("dbo.PhoneNumber", "PhoneNumberTypeId", "dbo.PhoneNumberType");
            DropForeignKey("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId", "dbo.ProfessionalLiabilityInsuranceOption");
            DropForeignKey("dbo.Section", "SectionProductId", "dbo.SectionProduct");
            DropIndex("dbo.AreaOfPractice", new[] { "AreaOfPracticeOptionId" });
            DropIndex("dbo.License", new[] { "LicensingPeriodId" });
            DropIndex("dbo.License", new[] { "LicenseTypeId" });
            DropIndex("dbo.Donation", new[] { "DonationProductId" });
            DropIndex("dbo.FinancialResponsibility", new[] { "CoveredByOptionId" });
            DropIndex("dbo.FirmSize", new[] { "FirmSizeOptionId" });
            DropIndex("dbo.JudicialPosition", new[] { "JudicialPositionOptionId" });
            DropIndex("dbo.Language", new[] { "LanguageOptionId" });
            DropIndex("dbo.LicenseTypeProduct", new[] { "LicensingProductId" });
            DropIndex("dbo.LicenseTypeSection", new[] { "SectionProductId" });
            DropIndex("dbo.PhoneNumber", new[] { "PhoneNumberTypeId" });
            DropIndex("dbo.ProfessionalLiabilityInsurance", new[] { "ProfessionalLiabilityInsuranceOptionId" });
            DropIndex("dbo.Section", new[] { "SectionProductId" });
            RenameColumn(table: "dbo.License", name: "LicenseTypeId", newName: "CurrentLicenseTypeId");
            AlterColumn("dbo.AreaOfPractice", "AreaOfPracticeOptionId", c => c.Int());
            AlterColumn("dbo.License", "LicensingPeriodId", c => c.Int());
            AlterColumn("dbo.License", "CurrentLicenseTypeId", c => c.Int());
            AlterColumn("dbo.Donation", "DonationProductId", c => c.Int());
            AlterColumn("dbo.FinancialResponsibility", "CoveredByOptionId", c => c.Int());
            AlterColumn("dbo.FirmSize", "FirmSizeOptionId", c => c.Int());
            AlterColumn("dbo.JudicialPosition", "JudicialPositionOptionId", c => c.Int());
            AlterColumn("dbo.Language", "LanguageOptionId", c => c.Int());
            AlterColumn("dbo.LicenseTypeProduct", "LicensingProductId", c => c.Int());
            AlterColumn("dbo.LicenseTypeSection", "SectionProductId", c => c.Int());
            AlterColumn("dbo.PhoneNumber", "PhoneNumberTypeId", c => c.Int());
            AlterColumn("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId", c => c.Int());
            AlterColumn("dbo.Section", "SectionProductId", c => c.Int());
            CreateIndex("dbo.AreaOfPractice", "AreaOfPracticeOptionId");
            CreateIndex("dbo.License", "LicensingPeriodId");
            CreateIndex("dbo.License", "CurrentLicenseTypeId");
            CreateIndex("dbo.Donation", "DonationProductId");
            CreateIndex("dbo.FinancialResponsibility", "CoveredByOptionId");
            CreateIndex("dbo.FirmSize", "FirmSizeOptionId");
            CreateIndex("dbo.JudicialPosition", "JudicialPositionOptionId");
            CreateIndex("dbo.Language", "LanguageOptionId");
            CreateIndex("dbo.LicenseTypeProduct", "LicensingProductId");
            CreateIndex("dbo.LicenseTypeSection", "SectionProductId");
            CreateIndex("dbo.PhoneNumber", "PhoneNumberTypeId");
            CreateIndex("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId");
            CreateIndex("dbo.Section", "SectionProductId");
            AddForeignKey("dbo.AreaOfPractice", "AreaOfPracticeOptionId", "dbo.AreaOfPracticeOption", "AreaOfPracticeOptionId");
            AddForeignKey("dbo.License", "CurrentLicenseTypeId", "dbo.LicenseType", "LicenseTypeId");
            AddForeignKey("dbo.License", "LicensingPeriodId", "dbo.LicensingPeriod", "LicensingPeriodId");
            AddForeignKey("dbo.Donation", "DonationProductId", "dbo.DonationProduct", "DonationProductId");
            AddForeignKey("dbo.FinancialResponsibility", "CoveredByOptionId", "dbo.CoveredByOption", "CoveredByOptionId");
            AddForeignKey("dbo.FirmSize", "FirmSizeOptionId", "dbo.FirmSizeOption", "FirmSizeOptionId");
            AddForeignKey("dbo.JudicialPosition", "JudicialPositionOptionId", "dbo.JudicialPositionOption", "JudicialPositionOptionId");
            AddForeignKey("dbo.Language", "LanguageOptionId", "dbo.LanguageOption", "LanguageOptionId");
            AddForeignKey("dbo.LicenseTypeProduct", "LicensingProductId", "dbo.LicensingProduct", "LicensingProductId");
            AddForeignKey("dbo.LicenseTypeSection", "SectionProductId", "dbo.SectionProduct", "SectionProductId");
            AddForeignKey("dbo.PhoneNumber", "PhoneNumberTypeId", "dbo.PhoneNumberType", "PhoneNumberTypeId");
            AddForeignKey("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId", "dbo.ProfessionalLiabilityInsuranceOption", "ProfessionalLiabilityInsuranceOptionId");
            AddForeignKey("dbo.Section", "SectionProductId", "dbo.SectionProduct", "SectionProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Section", "SectionProductId", "dbo.SectionProduct");
            DropForeignKey("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId", "dbo.ProfessionalLiabilityInsuranceOption");
            DropForeignKey("dbo.PhoneNumber", "PhoneNumberTypeId", "dbo.PhoneNumberType");
            DropForeignKey("dbo.LicenseTypeSection", "SectionProductId", "dbo.SectionProduct");
            DropForeignKey("dbo.LicenseTypeProduct", "LicensingProductId", "dbo.LicensingProduct");
            DropForeignKey("dbo.Language", "LanguageOptionId", "dbo.LanguageOption");
            DropForeignKey("dbo.JudicialPosition", "JudicialPositionOptionId", "dbo.JudicialPositionOption");
            DropForeignKey("dbo.FirmSize", "FirmSizeOptionId", "dbo.FirmSizeOption");
            DropForeignKey("dbo.FinancialResponsibility", "CoveredByOptionId", "dbo.CoveredByOption");
            DropForeignKey("dbo.Donation", "DonationProductId", "dbo.DonationProduct");
            DropForeignKey("dbo.License", "LicensingPeriodId", "dbo.LicensingPeriod");
            DropForeignKey("dbo.License", "CurrentLicenseTypeId", "dbo.LicenseType");
            DropForeignKey("dbo.AreaOfPractice", "AreaOfPracticeOptionId", "dbo.AreaOfPracticeOption");
            DropIndex("dbo.Section", new[] { "SectionProductId" });
            DropIndex("dbo.ProfessionalLiabilityInsurance", new[] { "ProfessionalLiabilityInsuranceOptionId" });
            DropIndex("dbo.PhoneNumber", new[] { "PhoneNumberTypeId" });
            DropIndex("dbo.LicenseTypeSection", new[] { "SectionProductId" });
            DropIndex("dbo.LicenseTypeProduct", new[] { "LicensingProductId" });
            DropIndex("dbo.Language", new[] { "LanguageOptionId" });
            DropIndex("dbo.JudicialPosition", new[] { "JudicialPositionOptionId" });
            DropIndex("dbo.FirmSize", new[] { "FirmSizeOptionId" });
            DropIndex("dbo.FinancialResponsibility", new[] { "CoveredByOptionId" });
            DropIndex("dbo.Donation", new[] { "DonationProductId" });
            DropIndex("dbo.License", new[] { "CurrentLicenseTypeId" });
            DropIndex("dbo.License", new[] { "LicensingPeriodId" });
            DropIndex("dbo.AreaOfPractice", new[] { "AreaOfPracticeOptionId" });
            AlterColumn("dbo.Section", "SectionProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneNumber", "PhoneNumberTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.LicenseTypeSection", "SectionProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.LicenseTypeProduct", "LicensingProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Language", "LanguageOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.JudicialPosition", "JudicialPositionOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.FirmSize", "FirmSizeOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.FinancialResponsibility", "CoveredByOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Donation", "DonationProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "CurrentLicenseTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "LicensingPeriodId", c => c.Int(nullable: false));
            AlterColumn("dbo.AreaOfPractice", "AreaOfPracticeOptionId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.License", name: "CurrentLicenseTypeId", newName: "LicenseTypeId");
            CreateIndex("dbo.Section", "SectionProductId");
            CreateIndex("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId");
            CreateIndex("dbo.PhoneNumber", "PhoneNumberTypeId");
            CreateIndex("dbo.LicenseTypeSection", "SectionProductId");
            CreateIndex("dbo.LicenseTypeProduct", "LicensingProductId");
            CreateIndex("dbo.Language", "LanguageOptionId");
            CreateIndex("dbo.JudicialPosition", "JudicialPositionOptionId");
            CreateIndex("dbo.FirmSize", "FirmSizeOptionId");
            CreateIndex("dbo.FinancialResponsibility", "CoveredByOptionId");
            CreateIndex("dbo.Donation", "DonationProductId");
            CreateIndex("dbo.License", "LicenseTypeId");
            CreateIndex("dbo.License", "LicensingPeriodId");
            CreateIndex("dbo.AreaOfPractice", "AreaOfPracticeOptionId");
            AddForeignKey("dbo.Section", "SectionProductId", "dbo.SectionProduct", "SectionProductId", cascadeDelete: true);
            AddForeignKey("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId", "dbo.ProfessionalLiabilityInsuranceOption", "ProfessionalLiabilityInsuranceOptionId", cascadeDelete: true);
            AddForeignKey("dbo.PhoneNumber", "PhoneNumberTypeId", "dbo.PhoneNumberType", "PhoneNumberTypeId", cascadeDelete: true);
            AddForeignKey("dbo.LicenseTypeSection", "SectionProductId", "dbo.SectionProduct", "SectionProductId", cascadeDelete: true);
            AddForeignKey("dbo.LicenseTypeProduct", "LicensingProductId", "dbo.LicensingProduct", "LicensingProductId", cascadeDelete: true);
            AddForeignKey("dbo.Language", "LanguageOptionId", "dbo.LanguageOption", "LanguageOptionId", cascadeDelete: true);
            AddForeignKey("dbo.JudicialPosition", "JudicialPositionOptionId", "dbo.JudicialPositionOption", "JudicialPositionOptionId", cascadeDelete: true);
            AddForeignKey("dbo.FirmSize", "FirmSizeOptionId", "dbo.FirmSizeOption", "FirmSizeOptionId", cascadeDelete: true);
            AddForeignKey("dbo.FinancialResponsibility", "CoveredByOptionId", "dbo.CoveredByOption", "CoveredByOptionId", cascadeDelete: true);
            AddForeignKey("dbo.Donation", "DonationProductId", "dbo.DonationProduct", "DonationProductId", cascadeDelete: true);
            AddForeignKey("dbo.License", "LicensingPeriodId", "dbo.LicensingPeriod", "LicensingPeriodId", cascadeDelete: true);
            AddForeignKey("dbo.License", "LicenseTypeId", "dbo.LicenseType", "LicenseTypeId", cascadeDelete: true);
            AddForeignKey("dbo.AreaOfPractice", "AreaOfPracticeOptionId", "dbo.AreaOfPracticeOption", "AreaOfPracticeOptionId", cascadeDelete: true);
        }
    }
}
