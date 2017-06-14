namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingkellerdeductionpercentagefromlicensetypeaddingkellerdiscounttableaddingrelationshiptolicensetype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KellerDiscount",
                c => new
                    {
                        KellerDiscountId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsProductDiscountId = c.String(),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KellerDiscountId);
            
            AddColumn("dbo.LicenseType", "KellerDiscountId", c => c.Int());
            CreateIndex("dbo.LicenseType", "KellerDiscountId");
            AddForeignKey("dbo.LicenseType", "KellerDiscountId", "dbo.KellerDiscount", "KellerDiscountId");
            DropColumn("dbo.LicenseType", "KellerDeductionPercentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LicenseType", "KellerDeductionPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.LicenseType", "KellerDiscountId", "dbo.KellerDiscount");
            DropIndex("dbo.LicenseType", new[] { "KellerDiscountId" });
            DropColumn("dbo.LicenseType", "KellerDiscountId");
            DropTable("dbo.KellerDiscount");
        }
    }
}
