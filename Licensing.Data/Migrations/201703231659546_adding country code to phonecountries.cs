namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcountrycodetophonecountries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhoneNumberCountry", "CountryCode", c => c.String());
            AddColumn("dbo.PhoneNumberCountry", "InternationalCode", c => c.String());
            DropColumn("dbo.PhoneNumberCountry", "AmsCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneNumberCountry", "AmsCode", c => c.String());
            DropColumn("dbo.PhoneNumberCountry", "InternationalCode");
            DropColumn("dbo.PhoneNumberCountry", "CountryCode");
        }
    }
}
