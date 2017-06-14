namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ithoughtvirtualstuffdidntmatterr : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.LicenseProductPrice", "LicenseProductId");
            AddForeignKey("dbo.LicenseProductPrice", "LicenseProductId", "dbo.LicenseProduct", "LicenseProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LicenseProductPrice", "LicenseProductId", "dbo.LicenseProduct");
            DropIndex("dbo.LicenseProductPrice", new[] { "LicenseProductId" });
        }
    }
}
