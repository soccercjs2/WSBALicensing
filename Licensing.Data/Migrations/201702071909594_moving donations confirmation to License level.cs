namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movingdonationsconfirmationtoLicenselevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "DonationsConfirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Donation", "Confirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Donation", "Confirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.License", "DonationsConfirmed");
        }
    }
}
