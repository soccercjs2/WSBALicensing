namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movingconfirmationforliststhatcanbeemptyandcompletetolicenselevelalsomovingoptoutstolicenselevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "AreasOfPracticeConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "LanguagesConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "DisabilityOptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "EthnicityOptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "GenderOptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "SexualOrientationOptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "SectionsConfirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.AreaOfPractice", "Confirmed");
            DropColumn("dbo.Disability", "OptedOut");
            DropColumn("dbo.Ethnicity", "OptedOut");
            DropColumn("dbo.Gender", "OptedOut");
            DropColumn("dbo.Language", "Confirmed");
            DropColumn("dbo.Section", "Confirmed");
            DropColumn("dbo.SexualOrientation", "OptedOut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SexualOrientation", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Section", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Language", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gender", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ethnicity", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Disability", "OptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.AreaOfPractice", "Confirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.License", "SectionsConfirmed");
            DropColumn("dbo.License", "SexualOrientationOptedOut");
            DropColumn("dbo.License", "GenderOptedOut");
            DropColumn("dbo.License", "EthnicityOptedOut");
            DropColumn("dbo.License", "DisabilityOptedOut");
            DropColumn("dbo.License", "LanguagesConfirmed");
            DropColumn("dbo.License", "AreasOfPracticeConfirmed");
        }
    }
}
