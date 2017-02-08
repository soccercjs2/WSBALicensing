namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingpreviouslicensetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "PreviousLicenseTypeId", c => c.Int());
            CreateIndex("dbo.License", "PreviousLicenseTypeId");
            AddForeignKey("dbo.License", "PreviousLicenseTypeId", "dbo.LicenseType", "LicenseTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.License", "PreviousLicenseTypeId", "dbo.LicenseType");
            DropIndex("dbo.License", new[] { "PreviousLicenseTypeId" });
            DropColumn("dbo.License", "PreviousLicenseTypeId");
        }
    }
}
