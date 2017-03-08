namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingoptout : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.License", "DisabilityOptedOut");
            DropColumn("dbo.License", "EthnicityOptedOut");
            DropColumn("dbo.License", "GenderOptedOut");
            DropColumn("dbo.License", "SexualOrientationOptedOut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.License", "SexualOrientationOptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "GenderOptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "EthnicityOptedOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.License", "DisabilityOptedOut", c => c.Boolean(nullable: false));
        }
    }
}
