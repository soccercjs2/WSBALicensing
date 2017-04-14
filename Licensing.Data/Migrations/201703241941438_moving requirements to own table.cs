namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movingrequirementstoowntable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LicenseTypeRequirement",
                c => new
                    {
                        LicenseTypeRequirementId = c.Int(nullable: false, identity: true),
                        LicenseTypeId = c.Int(nullable: false),
                        MembershipType = c.Int(nullable: false),
                        JudicialPosition = c.Int(nullable: false),
                        PracticeAreas = c.Int(nullable: false),
                        TrustAccount = c.Int(nullable: false),
                        ProfessionalLiabilityInsurance = c.Int(nullable: false),
                        FinancialResponsibility = c.Int(nullable: false),
                        ProBono = c.Int(nullable: false),
                        PrimaryAddress = c.Int(nullable: false),
                        HomeAddress = c.Int(nullable: false),
                        AgentOfServiceAddress = c.Int(nullable: false),
                        PrimaryEmail = c.Int(nullable: false),
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
                .PrimaryKey(t => t.LicenseTypeRequirementId);
            
            AddColumn("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId", c => c.Int());
            CreateIndex("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId");
            AddForeignKey("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId", "dbo.LicenseTypeRequirement", "LicenseTypeRequirementId");
            DropColumn("dbo.LicenseType", "MembershipType");
            DropColumn("dbo.LicenseType", "JudicialPosition");
            DropColumn("dbo.LicenseType", "PracticeAreas");
            DropColumn("dbo.LicenseType", "TrustAccount");
            DropColumn("dbo.LicenseType", "ProfessionalLiabilityInsurance");
            DropColumn("dbo.LicenseType", "FinancialResponsibility");
            DropColumn("dbo.LicenseType", "ProBono");
            DropColumn("dbo.LicenseType", "PrimaryAddress");
            DropColumn("dbo.LicenseType", "HomeAddress");
            DropColumn("dbo.LicenseType", "AgentOfServiceAddress");
            DropColumn("dbo.LicenseType", "PrimaryEmail");
            DropColumn("dbo.LicenseType", "PrimaryPhoneNumber");
            DropColumn("dbo.LicenseType", "HomePhoneNumber");
            DropColumn("dbo.LicenseType", "FaxPhoneNumber");
            DropColumn("dbo.LicenseType", "AreasOfPractice");
            DropColumn("dbo.LicenseType", "FirmSize");
            DropColumn("dbo.LicenseType", "Languages");
            DropColumn("dbo.LicenseType", "Disability");
            DropColumn("dbo.LicenseType", "Ethnicity");
            DropColumn("dbo.LicenseType", "Gender");
            DropColumn("dbo.LicenseType", "SexualOrientation");
            DropColumn("dbo.LicenseType", "Donations");
            DropColumn("dbo.LicenseType", "Sections");
            DropColumn("dbo.LicenseType", "BarNews");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LicenseType", "BarNews", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "Sections", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "Donations", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "SexualOrientation", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "Ethnicity", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "Disability", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "Languages", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "FirmSize", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "AreasOfPractice", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "FaxPhoneNumber", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "HomePhoneNumber", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "PrimaryPhoneNumber", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "PrimaryEmail", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "AgentOfServiceAddress", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "HomeAddress", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "PrimaryAddress", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "ProBono", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "FinancialResponsibility", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "ProfessionalLiabilityInsurance", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "TrustAccount", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "PracticeAreas", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "JudicialPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseType", "MembershipType", c => c.Int(nullable: false));
            DropForeignKey("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId", "dbo.LicenseTypeRequirement");
            DropIndex("dbo.LicenseType", new[] { "LicenseTypeRequirement_LicenseTypeRequirementId" });
            DropColumn("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId");
            DropTable("dbo.LicenseTypeRequirement");
        }
    }
}
