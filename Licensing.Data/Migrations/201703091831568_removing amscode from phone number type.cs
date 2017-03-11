namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingamscodefromphonenumbertype : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PhoneNumberType", "AmsCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneNumberType", "AmsCode", c => c.String());
        }
    }
}
