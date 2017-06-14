namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlatefeepercentagetolicensetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseType", "LateFeePercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LicenseType", "LateFeePercentage");
        }
    }
}
