namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAmsBasisKeytoLicenseProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseProduct", "AmsBasisKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LicenseProduct", "AmsBasisKey");
        }
    }
}
