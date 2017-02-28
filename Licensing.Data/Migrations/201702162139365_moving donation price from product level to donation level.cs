namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movingdonationpricefromproductleveltodonationlevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donation", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.DonationProduct", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DonationProduct", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Donation", "Amount");
        }
    }
}
