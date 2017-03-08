namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingactivatabletocodes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressType", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.AreaOfPracticeOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.CoveredByOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.DisabilityOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.DonationProduct", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.EthnicityOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.FirmSizeOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.GenderOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.JudicialPositionOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.LanguageOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.LicenseProduct", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.SectionProduct", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.PhoneNumberType", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.PracticeAreaOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProfessionalLiabilityInsuranceOption", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.SexualOrientationOption", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SexualOrientationOption", "Active");
            DropColumn("dbo.ProfessionalLiabilityInsuranceOption", "Active");
            DropColumn("dbo.PracticeAreaOption", "Active");
            DropColumn("dbo.PhoneNumberType", "Active");
            DropColumn("dbo.SectionProduct", "Active");
            DropColumn("dbo.LicenseProduct", "Active");
            DropColumn("dbo.LanguageOption", "Active");
            DropColumn("dbo.JudicialPositionOption", "Active");
            DropColumn("dbo.GenderOption", "Active");
            DropColumn("dbo.FirmSizeOption", "Active");
            DropColumn("dbo.EthnicityOption", "Active");
            DropColumn("dbo.DonationProduct", "Active");
            DropColumn("dbo.DisabilityOption", "Active");
            DropColumn("dbo.CoveredByOption", "Active");
            DropColumn("dbo.AreaOfPracticeOption", "Active");
            DropColumn("dbo.AddressType", "Active");
        }
    }
}
