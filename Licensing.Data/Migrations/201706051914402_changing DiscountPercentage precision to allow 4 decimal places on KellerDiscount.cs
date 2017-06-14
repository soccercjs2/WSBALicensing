namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingDiscountPercentageprecisiontoallow4decimalplacesonKellerDiscount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KellerDiscount", "DiscountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KellerDiscount", "DiscountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
