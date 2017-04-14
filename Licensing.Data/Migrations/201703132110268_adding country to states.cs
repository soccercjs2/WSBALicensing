namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcountrytostates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressState", "AmsCountryCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddressState", "AmsCountryCode");
        }
    }
}
