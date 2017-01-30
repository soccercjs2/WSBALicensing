namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LicensingTypeshouldntbepreloadable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LicenseType", "Confirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LicenseType", "Confirmed", c => c.Boolean(nullable: false));
        }
    }
}
