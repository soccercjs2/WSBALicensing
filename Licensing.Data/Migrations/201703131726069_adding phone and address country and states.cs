namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingphoneandaddresscountryandstates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressCountry",
                c => new
                    {
                        AddressCountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AddressCountryId);
            
            CreateTable(
                "dbo.AddressState",
                c => new
                    {
                        AddressStateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AddressStateId);
            
            CreateTable(
                "dbo.PhoneNumberCountry",
                c => new
                    {
                        PhoneNumberCountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneNumberCountryId);
            
            AddColumn("dbo.Address", "AddressStateId", c => c.Int());
            AddColumn("dbo.Address", "AddressCountryId", c => c.Int());
            AddColumn("dbo.PhoneNumber", "PhoneNumberCountryId", c => c.Int());
            CreateIndex("dbo.Address", "AddressStateId");
            CreateIndex("dbo.Address", "AddressCountryId");
            CreateIndex("dbo.PhoneNumber", "PhoneNumberCountryId");
            AddForeignKey("dbo.Address", "AddressCountryId", "dbo.AddressCountry", "AddressCountryId");
            AddForeignKey("dbo.Address", "AddressStateId", "dbo.AddressState", "AddressStateId");
            AddForeignKey("dbo.PhoneNumber", "PhoneNumberCountryId", "dbo.PhoneNumberCountry", "PhoneNumberCountryId");
            DropColumn("dbo.Address", "State");
            DropColumn("dbo.Address", "Country");
            DropColumn("dbo.PhoneNumber", "CountryCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneNumber", "CountryCode", c => c.Int(nullable: false));
            AddColumn("dbo.Address", "Country", c => c.String());
            AddColumn("dbo.Address", "State", c => c.String());
            DropForeignKey("dbo.PhoneNumber", "PhoneNumberCountryId", "dbo.PhoneNumberCountry");
            DropForeignKey("dbo.Address", "AddressStateId", "dbo.AddressState");
            DropForeignKey("dbo.Address", "AddressCountryId", "dbo.AddressCountry");
            DropIndex("dbo.PhoneNumber", new[] { "PhoneNumberCountryId" });
            DropIndex("dbo.Address", new[] { "AddressCountryId" });
            DropIndex("dbo.Address", new[] { "AddressStateId" });
            DropColumn("dbo.PhoneNumber", "PhoneNumberCountryId");
            DropColumn("dbo.Address", "AddressCountryId");
            DropColumn("dbo.Address", "AddressStateId");
            DropTable("dbo.PhoneNumberCountry");
            DropTable("dbo.AddressState");
            DropTable("dbo.AddressCountry");
        }
    }
}
