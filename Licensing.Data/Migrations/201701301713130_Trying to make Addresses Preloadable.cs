namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingtomakeAddressesPreloadable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "Confirmed");
        }
    }
}
