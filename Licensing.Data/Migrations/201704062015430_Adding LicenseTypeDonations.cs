namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLicenseTypeDonations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LicenseTypeDonation",
                c => new
                    {
                        LicenseTypeDonationId = c.Int(nullable: false, identity: true),
                        LicenseTypeId = c.Int(nullable: false),
                        DonationProductId = c.Int(),
                    })
                .PrimaryKey(t => t.LicenseTypeDonationId)
                .ForeignKey("dbo.DonationProduct", t => t.DonationProductId)
                .ForeignKey("dbo.LicenseType", t => t.LicenseTypeId, cascadeDelete: true)
                .Index(t => t.LicenseTypeId)
                .Index(t => t.DonationProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LicenseTypeDonation", "LicenseTypeId", "dbo.LicenseType");
            DropForeignKey("dbo.LicenseTypeDonation", "DonationProductId", "dbo.DonationProduct");
            DropIndex("dbo.LicenseTypeDonation", new[] { "DonationProductId" });
            DropIndex("dbo.LicenseTypeDonation", new[] { "LicenseTypeId" });
            DropTable("dbo.LicenseTypeDonation");
        }
    }
}
