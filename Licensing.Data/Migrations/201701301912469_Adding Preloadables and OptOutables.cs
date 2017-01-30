namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPreloadablesandOptOutables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AreaOfPractice", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Disability", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Donation", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Email", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ethnicity", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.FinancialResponsibility", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.FirmSize", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gender", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.JudicialPosition", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Language", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.LicenseType", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PhoneNumber", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProfessionalLiabilityInsurance", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Section", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.SexualOrientation", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.TrustAccount", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrustAccount", "Confirmed");
            DropColumn("dbo.SexualOrientation", "OptedOut");
            DropColumn("dbo.Section", "Confirmed");
            DropColumn("dbo.ProfessionalLiabilityInsurance", "Confirmed");
            DropColumn("dbo.PhoneNumber", "Confirmed");
            DropColumn("dbo.LicenseType", "Confirmed");
            DropColumn("dbo.Language", "Confirmed");
            DropColumn("dbo.JudicialPosition", "Confirmed");
            DropColumn("dbo.Gender", "OptedOut");
            DropColumn("dbo.FirmSize", "Confirmed");
            DropColumn("dbo.FinancialResponsibility", "Confirmed");
            DropColumn("dbo.Ethnicity", "OptedOut");
            DropColumn("dbo.Email", "Confirmed");
            DropColumn("dbo.Donation", "Confirmed");
            DropColumn("dbo.Disability", "OptedOut");
            DropColumn("dbo.AreaOfPractice", "Confirmed");
        }
    }
}
