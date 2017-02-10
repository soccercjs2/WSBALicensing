namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseTypeProduct", "LicenseProductId", c => c.Int());
            CreateIndex("dbo.LicenseTypeProduct", "LicenseProductId");
            AddForeignKey("dbo.LicenseTypeProduct", "LicenseProductId", "dbo.LicenseProduct", "LicenseProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LicenseTypeProduct", "LicenseProductId", "dbo.LicenseProduct");
            DropIndex("dbo.LicenseTypeProduct", new[] { "LicenseProductId" });
            DropColumn("dbo.LicenseTypeProduct", "LicenseProductId");
        }
    }
}
