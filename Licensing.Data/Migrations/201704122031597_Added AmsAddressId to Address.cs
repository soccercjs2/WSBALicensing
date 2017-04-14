namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAmsAddressIdtoAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "AmsAddressId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "AmsAddressId");
        }
    }
}
