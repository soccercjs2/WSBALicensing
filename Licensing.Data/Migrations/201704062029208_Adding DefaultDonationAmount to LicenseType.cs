namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDefaultDonationAmounttoLicenseType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseType", "DefaultDonationAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LicenseType", "DefaultDonationAmount");
        }
    }
}
