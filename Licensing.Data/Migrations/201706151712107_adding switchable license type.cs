namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingswitchablelicensetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "SwitchableLicenseTypeId", c => c.Int());
            CreateIndex("dbo.License", "SwitchableLicenseTypeId");
            AddForeignKey("dbo.License", "SwitchableLicenseTypeId", "dbo.LicenseType", "LicenseTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.License", "SwitchableLicenseTypeId", "dbo.LicenseType");
            DropIndex("dbo.License", new[] { "SwitchableLicenseTypeId" });
            DropColumn("dbo.License", "SwitchableLicenseTypeId");
        }
    }
}
