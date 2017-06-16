namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movingSwitchableLicenseTypefromLicensetoLicenseType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.License", "SwitchableLicenseTypeId", "dbo.LicenseType");
            DropIndex("dbo.License", new[] { "SwitchableLicenseTypeId" });
            AddColumn("dbo.LicenseType", "SwitchableLicenseTypeId", c => c.Int());
            CreateIndex("dbo.LicenseType", "SwitchableLicenseTypeId");
            AddForeignKey("dbo.LicenseType", "SwitchableLicenseTypeId", "dbo.LicenseType", "LicenseTypeId");
            DropColumn("dbo.License", "SwitchableLicenseTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.License", "SwitchableLicenseTypeId", c => c.Int());
            DropForeignKey("dbo.LicenseType", "SwitchableLicenseTypeId", "dbo.LicenseType");
            DropIndex("dbo.LicenseType", new[] { "SwitchableLicenseTypeId" });
            DropColumn("dbo.LicenseType", "SwitchableLicenseTypeId");
            CreateIndex("dbo.License", "SwitchableLicenseTypeId");
            AddForeignKey("dbo.License", "SwitchableLicenseTypeId", "dbo.LicenseType", "LicenseTypeId");
        }
    }
}
