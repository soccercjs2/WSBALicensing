namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingrequirementnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LicenseType", "LicenseTypeRequirementId", "dbo.LicenseTypeRequirement");
            DropIndex("dbo.LicenseType", new[] { "LicenseTypeRequirementId" });
            AlterColumn("dbo.LicenseType", "LicenseTypeRequirementId", c => c.Int());
            CreateIndex("dbo.LicenseType", "LicenseTypeRequirementId");
            AddForeignKey("dbo.LicenseType", "LicenseTypeRequirementId", "dbo.LicenseTypeRequirement", "LicenseTypeRequirementId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LicenseType", "LicenseTypeRequirementId", "dbo.LicenseTypeRequirement");
            DropIndex("dbo.LicenseType", new[] { "LicenseTypeRequirementId" });
            AlterColumn("dbo.LicenseType", "LicenseTypeRequirementId", c => c.Int(nullable: false));
            CreateIndex("dbo.LicenseType", "LicenseTypeRequirementId");
            AddForeignKey("dbo.LicenseType", "LicenseTypeRequirementId", "dbo.LicenseTypeRequirement", "LicenseTypeRequirementId", cascadeDelete: true);
        }
    }
}
