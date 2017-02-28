namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingdescriptiontodonationproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationProduct", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonationProduct", "Description");
        }
    }
}
