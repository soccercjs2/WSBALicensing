namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingmclerequirement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseTypeRequirement", "MCLE", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LicenseTypeRequirement", "MCLE");
        }
    }
}
