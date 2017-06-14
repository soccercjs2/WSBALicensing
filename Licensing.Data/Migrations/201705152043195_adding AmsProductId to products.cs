namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAmsProductIdtoproducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationProduct", "AmsProductId", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseProduct", "AmsProductId", c => c.Int(nullable: false));
            AddColumn("dbo.SectionProduct", "AmsProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SectionProduct", "AmsProductId");
            DropColumn("dbo.LicenseProduct", "AmsProductId");
            DropColumn("dbo.DonationProduct", "AmsProductId");
        }
    }
}
