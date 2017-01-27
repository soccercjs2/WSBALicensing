namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Really : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        AddressTypeId = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.AddressType", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.AddressTypeId);
            
            CreateTable(
                "dbo.AddressType",
                c => new
                    {
                        AddressTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.AddressTypeId);
            
            CreateTable(
                "dbo.AreaOfPracticeOption",
                c => new
                    {
                        AreaOfPracticeOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.AreaOfPracticeOptionId);
            
            CreateTable(
                "dbo.AreaOfPractice",
                c => new
                    {
                        AreaOfPracticeId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        AreaOfPracticeOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaOfPracticeId)
                .ForeignKey("dbo.AreaOfPracticeOption", t => t.AreaOfPracticeOptionId, cascadeDelete: true)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.AreaOfPracticeOptionId);
            
            CreateTable(
                "dbo.CoveredByOption",
                c => new
                    {
                        CoveredByOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.CoveredByOptionId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        BarNumber = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.License",
                c => new
                    {
                        LicenseId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        LicensingPeriodId = c.Int(nullable: false),
                        LicenseTypeId = c.Int(nullable: false),
                        FinancialResponsibilityId = c.Int(nullable: false),
                        JudicialPositionId = c.Int(nullable: false),
                        ProBonoId = c.Int(nullable: false),
                        ProfessionalLiabilityInsuranceId = c.Int(nullable: false),
                        TrustAccountId = c.Int(nullable: false),
                        FirmSizeId = c.Int(nullable: false),
                        DisabilityId = c.Int(nullable: false),
                        EthnicityId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                        SexualOrientationId = c.Int(nullable: false),
                        BarNews = c.Boolean(),
                    })
                .PrimaryKey(t => t.LicenseId)
                .ForeignKey("dbo.Disability", t => t.DisabilityId, cascadeDelete: true)
                .ForeignKey("dbo.Ethnicity", t => t.EthnicityId, cascadeDelete: true)
                .ForeignKey("dbo.FinancialResponsibility", t => t.FinancialResponsibilityId, cascadeDelete: true)
                .ForeignKey("dbo.FirmSize", t => t.FirmSizeId, cascadeDelete: true)
                .ForeignKey("dbo.Gender", t => t.GenderId, cascadeDelete: true)
                .ForeignKey("dbo.JudicialPosition", t => t.JudicialPositionId, cascadeDelete: true)
                .ForeignKey("dbo.LicenseType", t => t.LicenseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.LicensingPeriod", t => t.LicensingPeriodId, cascadeDelete: true)
                .ForeignKey("dbo.ProBono", t => t.ProBonoId, cascadeDelete: true)
                .ForeignKey("dbo.ProfessionalLiabilityInsurance", t => t.ProfessionalLiabilityInsuranceId, cascadeDelete: true)
                .ForeignKey("dbo.SexualOrientation", t => t.SexualOrientationId, cascadeDelete: true)
                .ForeignKey("dbo.TrustAccount", t => t.TrustAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.LicensingPeriodId)
                .Index(t => t.LicenseTypeId)
                .Index(t => t.FinancialResponsibilityId)
                .Index(t => t.JudicialPositionId)
                .Index(t => t.ProBonoId)
                .Index(t => t.ProfessionalLiabilityInsuranceId)
                .Index(t => t.TrustAccountId)
                .Index(t => t.FirmSizeId)
                .Index(t => t.DisabilityId)
                .Index(t => t.EthnicityId)
                .Index(t => t.GenderId)
                .Index(t => t.SexualOrientationId);
            
            CreateTable(
                "dbo.Disability",
                c => new
                    {
                        DisabilityId = c.Int(nullable: false, identity: true),
                        DisabilityOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DisabilityId)
                .ForeignKey("dbo.DisabilityOption", t => t.DisabilityOptionId, cascadeDelete: true)
                .Index(t => t.DisabilityOptionId);
            
            CreateTable(
                "dbo.DisabilityOption",
                c => new
                    {
                        DisabilityOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.DisabilityOptionId);
            
            CreateTable(
                "dbo.Donation",
                c => new
                    {
                        DonationId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        DonationProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonationId)
                .ForeignKey("dbo.DonationProduct", t => t.DonationProductId, cascadeDelete: true)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.DonationProductId);
            
            CreateTable(
                "dbo.DonationProduct",
                c => new
                    {
                        DonationProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.DonationProductId);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        EmailTypeId = c.Int(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.EmailType", t => t.EmailTypeId, cascadeDelete: true)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.EmailTypeId);
            
            CreateTable(
                "dbo.EmailType",
                c => new
                    {
                        EmailTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.EmailTypeId);
            
            CreateTable(
                "dbo.Ethnicity",
                c => new
                    {
                        EthnicityId = c.Int(nullable: false, identity: true),
                        EthnicityOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EthnicityId)
                .ForeignKey("dbo.EthnicityOption", t => t.EthnicityOptionId, cascadeDelete: true)
                .Index(t => t.EthnicityOptionId);
            
            CreateTable(
                "dbo.EthnicityOption",
                c => new
                    {
                        EthnicityOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.EthnicityOptionId);
            
            CreateTable(
                "dbo.FinancialResponsibility",
                c => new
                    {
                        FinancialResponsibilityId = c.Int(nullable: false, identity: true),
                        Company = c.String(),
                        PolicyNumber = c.String(),
                        CoveredByOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FinancialResponsibilityId)
                .ForeignKey("dbo.CoveredByOption", t => t.CoveredByOptionId, cascadeDelete: true)
                .Index(t => t.CoveredByOptionId);
            
            CreateTable(
                "dbo.FirmSize",
                c => new
                    {
                        FirmSizeId = c.Int(nullable: false, identity: true),
                        FirmSizeOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FirmSizeId)
                .ForeignKey("dbo.FirmSizeOption", t => t.FirmSizeOptionId, cascadeDelete: true)
                .Index(t => t.FirmSizeOptionId);
            
            CreateTable(
                "dbo.FirmSizeOption",
                c => new
                    {
                        FirmSizeOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.FirmSizeOptionId);
            
            CreateTable(
                "dbo.Gender",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        GenderOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GenderId)
                .ForeignKey("dbo.GenderOption", t => t.GenderOptionId, cascadeDelete: true)
                .Index(t => t.GenderOptionId);
            
            CreateTable(
                "dbo.GenderOption",
                c => new
                    {
                        GenderOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.GenderOptionId);
            
            CreateTable(
                "dbo.JudicialPosition",
                c => new
                    {
                        JudicialPositionId = c.Int(nullable: false, identity: true),
                        JudicialPositionOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JudicialPositionId)
                .ForeignKey("dbo.JudicialPositionOption", t => t.JudicialPositionOptionId, cascadeDelete: true)
                .Index(t => t.JudicialPositionOptionId);
            
            CreateTable(
                "dbo.JudicialPositionOption",
                c => new
                    {
                        JudicialPositionOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.JudicialPositionOptionId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        LanguageOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageId)
                .ForeignKey("dbo.LanguageOption", t => t.LanguageOptionId, cascadeDelete: true)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.LanguageOptionId);
            
            CreateTable(
                "dbo.LanguageOption",
                c => new
                    {
                        LanguageOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.LanguageOptionId);
            
            CreateTable(
                "dbo.LicenseType",
                c => new
                    {
                        LicenseTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MembershipType = c.Int(nullable: false),
                        JudicialPosition = c.Int(nullable: false),
                        TrustAccount = c.Int(nullable: false),
                        ProfessionalLiabilityInsurance = c.Int(nullable: false),
                        FinancialResponsibility = c.Int(nullable: false),
                        ProBono = c.Int(nullable: false),
                        PrimaryAddress = c.Int(nullable: false),
                        HomeAddress = c.Int(nullable: false),
                        AgentOfServiceAddress = c.Int(nullable: false),
                        PrimaryEmail = c.Int(nullable: false),
                        HomeEmail = c.Int(nullable: false),
                        PrimaryPhoneNumber = c.Int(nullable: false),
                        HomePhoneNumber = c.Int(nullable: false),
                        FaxPhoneNumber = c.Int(nullable: false),
                        AreasOfPractice = c.Int(nullable: false),
                        FirmSize = c.Int(nullable: false),
                        Languages = c.Int(nullable: false),
                        Disability = c.Int(nullable: false),
                        Ethnicity = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        SexualOrientation = c.Int(nullable: false),
                        Donations = c.Int(nullable: false),
                        Sections = c.Int(nullable: false),
                        BarNews = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LicenseTypeId);
            
            CreateTable(
                "dbo.LicenseTypeProduct",
                c => new
                    {
                        LicenseTypeProductId = c.Int(nullable: false, identity: true),
                        LicenseTypeId = c.Int(nullable: false),
                        LicensingProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LicenseTypeProductId)
                .ForeignKey("dbo.LicensingProduct", t => t.LicensingProductId, cascadeDelete: true)
                .ForeignKey("dbo.LicenseType", t => t.LicenseTypeId, cascadeDelete: true)
                .Index(t => t.LicenseTypeId)
                .Index(t => t.LicensingProductId);
            
            CreateTable(
                "dbo.LicensingProduct",
                c => new
                    {
                        LicensingProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.LicensingProductId);
            
            CreateTable(
                "dbo.LicenseTypeSection",
                c => new
                    {
                        LicenseTypeSectionId = c.Int(nullable: false, identity: true),
                        LicenseTypeId = c.Int(nullable: false),
                        SectionProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LicenseTypeSectionId)
                .ForeignKey("dbo.SectionProduct", t => t.SectionProductId, cascadeDelete: true)
                .ForeignKey("dbo.LicenseType", t => t.LicenseTypeId, cascadeDelete: true)
                .Index(t => t.LicenseTypeId)
                .Index(t => t.SectionProductId);
            
            CreateTable(
                "dbo.SectionProduct",
                c => new
                    {
                        SectionProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.SectionProductId);
            
            CreateTable(
                "dbo.LicensingPeriod",
                c => new
                    {
                        LicensingPeriodId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        LateFeeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LicensingPeriodId);
            
            CreateTable(
                "dbo.PhoneNumber",
                c => new
                    {
                        PhoneNumberId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        PhoneNumberTypeId = c.Int(nullable: false),
                        CountryCode = c.Int(nullable: false),
                        AreaCode = c.Int(nullable: false),
                        ExchangeCode = c.Int(nullable: false),
                        LineNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneNumberId)
                .ForeignKey("dbo.PhoneNumberType", t => t.PhoneNumberTypeId, cascadeDelete: true)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.PhoneNumberTypeId);
            
            CreateTable(
                "dbo.PhoneNumberType",
                c => new
                    {
                        PhoneNumberTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.PhoneNumberTypeId);
            
            CreateTable(
                "dbo.ProBono",
                c => new
                    {
                        ProBonoId = c.Int(nullable: false, identity: true),
                        ProvidesService = c.Boolean(),
                        FreeServiceHours = c.Decimal(precision: 18, scale: 2),
                        LimitedFeeServiceHours = c.Decimal(precision: 18, scale: 2),
                        Anonymous = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProBonoId);
            
            CreateTable(
                "dbo.ProfessionalLiabilityInsurance",
                c => new
                    {
                        ProfessionalLiabilityInsuranceId = c.Int(nullable: false, identity: true),
                        ProfessionalLiabilityInsuranceOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProfessionalLiabilityInsuranceId)
                .ForeignKey("dbo.ProfessionalLiabilityInsuranceOption", t => t.ProfessionalLiabilityInsuranceOptionId, cascadeDelete: true)
                .Index(t => t.ProfessionalLiabilityInsuranceOptionId);
            
            CreateTable(
                "dbo.ProfessionalLiabilityInsuranceOption",
                c => new
                    {
                        ProfessionalLiabilityInsuranceOptionId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProfessionalLiabilityInsuranceOptionId);
            
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        SectionProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionId)
                .ForeignKey("dbo.SectionProduct", t => t.SectionProductId, cascadeDelete: true)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.SectionProductId);
            
            CreateTable(
                "dbo.SexualOrientation",
                c => new
                    {
                        SexualOrientationId = c.Int(nullable: false, identity: true),
                        SexualOrientationOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SexualOrientationId)
                .ForeignKey("dbo.SexualOrientationOption", t => t.SexualOrientationOptionId, cascadeDelete: true)
                .Index(t => t.SexualOrientationOptionId);
            
            CreateTable(
                "dbo.SexualOrientationOption",
                c => new
                    {
                        SexualOrientationOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.SexualOrientationOptionId);
            
            CreateTable(
                "dbo.TrustAccount",
                c => new
                    {
                        TrustAccountId = c.Int(nullable: false, identity: true),
                        HandlesTrustAccount = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TrustAccountId);
            
            CreateTable(
                "dbo.TrustAccountNumber",
                c => new
                    {
                        TrustAccountNumberId = c.Int(nullable: false, identity: true),
                        TrustAccountId = c.Int(nullable: false),
                        Bank = c.String(),
                        Branch = c.String(),
                        AccountNumber = c.String(),
                    })
                .PrimaryKey(t => t.TrustAccountNumberId)
                .ForeignKey("dbo.TrustAccount", t => t.TrustAccountId, cascadeDelete: true)
                .Index(t => t.TrustAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.License", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.License", "TrustAccountId", "dbo.TrustAccount");
            DropForeignKey("dbo.TrustAccountNumber", "TrustAccountId", "dbo.TrustAccount");
            DropForeignKey("dbo.License", "SexualOrientationId", "dbo.SexualOrientation");
            DropForeignKey("dbo.SexualOrientation", "SexualOrientationOptionId", "dbo.SexualOrientationOption");
            DropForeignKey("dbo.Section", "LicenseId", "dbo.License");
            DropForeignKey("dbo.Section", "SectionProductId", "dbo.SectionProduct");
            DropForeignKey("dbo.License", "ProfessionalLiabilityInsuranceId", "dbo.ProfessionalLiabilityInsurance");
            DropForeignKey("dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceOptionId", "dbo.ProfessionalLiabilityInsuranceOption");
            DropForeignKey("dbo.License", "ProBonoId", "dbo.ProBono");
            DropForeignKey("dbo.PhoneNumber", "LicenseId", "dbo.License");
            DropForeignKey("dbo.PhoneNumber", "PhoneNumberTypeId", "dbo.PhoneNumberType");
            DropForeignKey("dbo.License", "LicensingPeriodId", "dbo.LicensingPeriod");
            DropForeignKey("dbo.License", "LicenseTypeId", "dbo.LicenseType");
            DropForeignKey("dbo.LicenseTypeSection", "LicenseTypeId", "dbo.LicenseType");
            DropForeignKey("dbo.LicenseTypeSection", "SectionProductId", "dbo.SectionProduct");
            DropForeignKey("dbo.LicenseTypeProduct", "LicenseTypeId", "dbo.LicenseType");
            DropForeignKey("dbo.LicenseTypeProduct", "LicensingProductId", "dbo.LicensingProduct");
            DropForeignKey("dbo.Language", "LicenseId", "dbo.License");
            DropForeignKey("dbo.Language", "LanguageOptionId", "dbo.LanguageOption");
            DropForeignKey("dbo.License", "JudicialPositionId", "dbo.JudicialPosition");
            DropForeignKey("dbo.JudicialPosition", "JudicialPositionOptionId", "dbo.JudicialPositionOption");
            DropForeignKey("dbo.License", "GenderId", "dbo.Gender");
            DropForeignKey("dbo.Gender", "GenderOptionId", "dbo.GenderOption");
            DropForeignKey("dbo.License", "FirmSizeId", "dbo.FirmSize");
            DropForeignKey("dbo.FirmSize", "FirmSizeOptionId", "dbo.FirmSizeOption");
            DropForeignKey("dbo.License", "FinancialResponsibilityId", "dbo.FinancialResponsibility");
            DropForeignKey("dbo.FinancialResponsibility", "CoveredByOptionId", "dbo.CoveredByOption");
            DropForeignKey("dbo.License", "EthnicityId", "dbo.Ethnicity");
            DropForeignKey("dbo.Ethnicity", "EthnicityOptionId", "dbo.EthnicityOption");
            DropForeignKey("dbo.Email", "LicenseId", "dbo.License");
            DropForeignKey("dbo.Email", "EmailTypeId", "dbo.EmailType");
            DropForeignKey("dbo.Donation", "LicenseId", "dbo.License");
            DropForeignKey("dbo.Donation", "DonationProductId", "dbo.DonationProduct");
            DropForeignKey("dbo.License", "DisabilityId", "dbo.Disability");
            DropForeignKey("dbo.Disability", "DisabilityOptionId", "dbo.DisabilityOption");
            DropForeignKey("dbo.AreaOfPractice", "LicenseId", "dbo.License");
            DropForeignKey("dbo.Address", "LicenseId", "dbo.License");
            DropForeignKey("dbo.AreaOfPractice", "AreaOfPracticeOptionId", "dbo.AreaOfPracticeOption");
            DropForeignKey("dbo.Address", "AddressTypeId", "dbo.AddressType");
            DropIndex("dbo.TrustAccountNumber", new[] { "TrustAccountId" });
            DropIndex("dbo.SexualOrientation", new[] { "SexualOrientationOptionId" });
            DropIndex("dbo.Section", new[] { "SectionProductId" });
            DropIndex("dbo.Section", new[] { "LicenseId" });
            DropIndex("dbo.ProfessionalLiabilityInsurance", new[] { "ProfessionalLiabilityInsuranceOptionId" });
            DropIndex("dbo.PhoneNumber", new[] { "PhoneNumberTypeId" });
            DropIndex("dbo.PhoneNumber", new[] { "LicenseId" });
            DropIndex("dbo.LicenseTypeSection", new[] { "SectionProductId" });
            DropIndex("dbo.LicenseTypeSection", new[] { "LicenseTypeId" });
            DropIndex("dbo.LicenseTypeProduct", new[] { "LicensingProductId" });
            DropIndex("dbo.LicenseTypeProduct", new[] { "LicenseTypeId" });
            DropIndex("dbo.Language", new[] { "LanguageOptionId" });
            DropIndex("dbo.Language", new[] { "LicenseId" });
            DropIndex("dbo.JudicialPosition", new[] { "JudicialPositionOptionId" });
            DropIndex("dbo.Gender", new[] { "GenderOptionId" });
            DropIndex("dbo.FirmSize", new[] { "FirmSizeOptionId" });
            DropIndex("dbo.FinancialResponsibility", new[] { "CoveredByOptionId" });
            DropIndex("dbo.Ethnicity", new[] { "EthnicityOptionId" });
            DropIndex("dbo.Email", new[] { "EmailTypeId" });
            DropIndex("dbo.Email", new[] { "LicenseId" });
            DropIndex("dbo.Donation", new[] { "DonationProductId" });
            DropIndex("dbo.Donation", new[] { "LicenseId" });
            DropIndex("dbo.Disability", new[] { "DisabilityOptionId" });
            DropIndex("dbo.License", new[] { "SexualOrientationId" });
            DropIndex("dbo.License", new[] { "GenderId" });
            DropIndex("dbo.License", new[] { "EthnicityId" });
            DropIndex("dbo.License", new[] { "DisabilityId" });
            DropIndex("dbo.License", new[] { "FirmSizeId" });
            DropIndex("dbo.License", new[] { "TrustAccountId" });
            DropIndex("dbo.License", new[] { "ProfessionalLiabilityInsuranceId" });
            DropIndex("dbo.License", new[] { "ProBonoId" });
            DropIndex("dbo.License", new[] { "JudicialPositionId" });
            DropIndex("dbo.License", new[] { "FinancialResponsibilityId" });
            DropIndex("dbo.License", new[] { "LicenseTypeId" });
            DropIndex("dbo.License", new[] { "LicensingPeriodId" });
            DropIndex("dbo.License", new[] { "CustomerId" });
            DropIndex("dbo.AreaOfPractice", new[] { "AreaOfPracticeOptionId" });
            DropIndex("dbo.AreaOfPractice", new[] { "LicenseId" });
            DropIndex("dbo.Address", new[] { "AddressTypeId" });
            DropIndex("dbo.Address", new[] { "LicenseId" });
            DropTable("dbo.TrustAccountNumber");
            DropTable("dbo.TrustAccount");
            DropTable("dbo.SexualOrientationOption");
            DropTable("dbo.SexualOrientation");
            DropTable("dbo.Section");
            DropTable("dbo.ProfessionalLiabilityInsuranceOption");
            DropTable("dbo.ProfessionalLiabilityInsurance");
            DropTable("dbo.ProBono");
            DropTable("dbo.PhoneNumberType");
            DropTable("dbo.PhoneNumber");
            DropTable("dbo.LicensingPeriod");
            DropTable("dbo.SectionProduct");
            DropTable("dbo.LicenseTypeSection");
            DropTable("dbo.LicensingProduct");
            DropTable("dbo.LicenseTypeProduct");
            DropTable("dbo.LicenseType");
            DropTable("dbo.LanguageOption");
            DropTable("dbo.Language");
            DropTable("dbo.JudicialPositionOption");
            DropTable("dbo.JudicialPosition");
            DropTable("dbo.GenderOption");
            DropTable("dbo.Gender");
            DropTable("dbo.FirmSizeOption");
            DropTable("dbo.FirmSize");
            DropTable("dbo.FinancialResponsibility");
            DropTable("dbo.EthnicityOption");
            DropTable("dbo.Ethnicity");
            DropTable("dbo.EmailType");
            DropTable("dbo.Email");
            DropTable("dbo.DonationProduct");
            DropTable("dbo.Donation");
            DropTable("dbo.DisabilityOption");
            DropTable("dbo.Disability");
            DropTable("dbo.License");
            DropTable("dbo.Customer");
            DropTable("dbo.CoveredByOption");
            DropTable("dbo.AreaOfPractice");
            DropTable("dbo.AreaOfPracticeOption");
            DropTable("dbo.AddressType");
            DropTable("dbo.Address");
        }
    }
}
