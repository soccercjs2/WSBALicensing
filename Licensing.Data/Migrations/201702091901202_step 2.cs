namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LicenseTypeProduct", "LicensingProductId", "dbo.LicensingProduct");
            DropIndex("dbo.LicenseTypeProduct", new[] { "LicensingProductId" });
            CreateTable(
                "dbo.LicenseProduct",
                c => new
                    {
                        LicenseProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.LicenseProductId);
            
            DropColumn("dbo.LicenseTypeProduct", "LicensingProductId");
            DropTable("dbo.LicensingProduct");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LicensingProduct",
                c => new
                    {
                        LicensingProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.LicensingProductId);
            
            AddColumn("dbo.LicenseTypeProduct", "LicensingProductId", c => c.Int());
            DropTable("dbo.LicenseProduct");
            CreateIndex("dbo.LicenseTypeProduct", "LicensingProductId");
            AddForeignKey("dbo.LicenseTypeProduct", "LicensingProductId", "dbo.LicensingProduct", "LicensingProductId");
        }
    }
}
