namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingphonenumberpartsto1fieldandaddingextension : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhoneNumber", "Number", c => c.String());
            AddColumn("dbo.PhoneNumber", "Extension", c => c.String());
            DropColumn("dbo.PhoneNumber", "AreaCode");
            DropColumn("dbo.PhoneNumber", "ExchangeCode");
            DropColumn("dbo.PhoneNumber", "LineNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneNumber", "LineNumber", c => c.Int(nullable: false));
            AddColumn("dbo.PhoneNumber", "ExchangeCode", c => c.Int(nullable: false));
            AddColumn("dbo.PhoneNumber", "AreaCode", c => c.Int(nullable: false));
            DropColumn("dbo.PhoneNumber", "Extension");
            DropColumn("dbo.PhoneNumber", "Number");
        }
    }
}
