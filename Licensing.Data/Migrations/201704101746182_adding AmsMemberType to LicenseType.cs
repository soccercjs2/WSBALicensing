namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAmsMemberTypetoLicenseType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseType", "AmsMemberType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LicenseType", "AmsMemberType");
        }
    }
}
