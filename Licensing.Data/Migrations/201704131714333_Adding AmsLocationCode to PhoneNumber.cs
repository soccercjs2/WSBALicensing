namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAmsLocationCodetoPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhoneNumber", "AmsLocationCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhoneNumber", "AmsLocationCode");
        }
    }
}
