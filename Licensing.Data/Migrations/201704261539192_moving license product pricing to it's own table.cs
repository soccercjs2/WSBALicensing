namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movinglicenseproductpricingtoitsowntable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LicenseProductPrice",
                c => new
                    {
                        LicenseProductPriceId = c.Int(nullable: false, identity: true),
                        LicenseProductId = c.Int(nullable: false),
                        AmsBasisFrom = c.Decimal(precision: 18, scale: 2),
                        AmsBasisTo = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LicenseProductPriceId);
            
            DropColumn("dbo.LicenseProduct", "Price");
            DropColumn("dbo.LicenseProduct", "AmsBasisKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LicenseProduct", "AmsBasisKey", c => c.String());
            AddColumn("dbo.LicenseProduct", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.LicenseProductPrice");
        }
    }
}
