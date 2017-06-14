namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingprimaryandlatefeeflagstolicensetypeproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseTypeProduct", "PrimaryProduct", c => c.Boolean(nullable: false));
            AddColumn("dbo.LicenseTypeProduct", "LateFeeProduct", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LicenseTypeProduct", "LateFeeProduct");
            DropColumn("dbo.LicenseTypeProduct", "PrimaryProduct");
        }
    }
}
