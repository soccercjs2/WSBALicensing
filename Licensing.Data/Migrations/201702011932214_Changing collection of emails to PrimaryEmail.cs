namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingcollectionofemailstoPrimaryEmail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Email", "LicenseId", "dbo.License");
            DropIndex("dbo.Email", new[] { "LicenseId" });
            AddColumn("dbo.License", "EmailId", c => c.Int());
            CreateIndex("dbo.License", "EmailId");
            AddForeignKey("dbo.License", "EmailId", "dbo.Email", "EmailId");
            DropColumn("dbo.LicenseType", "HomeEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LicenseType", "HomeEmail", c => c.Int(nullable: false));
            DropForeignKey("dbo.License", "EmailId", "dbo.Email");
            DropIndex("dbo.License", new[] { "EmailId" });
            DropColumn("dbo.License", "EmailId");
            CreateIndex("dbo.Email", "LicenseId");
            AddForeignKey("dbo.Email", "LicenseId", "dbo.License", "LicenseId", cascadeDelete: true);
        }
    }
}
