namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingcurrentlictypetolictype : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.License", name: "CurrentLicenseTypeId", newName: "LicenseTypeId");
            RenameIndex(table: "dbo.License", name: "IX_CurrentLicenseTypeId", newName: "IX_LicenseTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.License", name: "IX_LicenseTypeId", newName: "IX_CurrentLicenseTypeId");
            RenameColumn(table: "dbo.License", name: "LicenseTypeId", newName: "CurrentLicenseTypeId");
        }
    }
}
