namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingsomenavigationbetweenlicensetypeandrequirements : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId", "dbo.LicenseTypeRequirement");
            DropIndex("dbo.LicenseType", new[] { "LicenseTypeRequirement_LicenseTypeRequirementId" });
            RenameColumn(table: "dbo.LicenseType", name: "LicenseTypeRequirement_LicenseTypeRequirementId", newName: "LicenseTypeRequirementId");
            AlterColumn("dbo.LicenseType", "LicenseTypeRequirementId", c => c.Int(nullable: false));
            CreateIndex("dbo.LicenseType", "LicenseTypeRequirementId");
            AddForeignKey("dbo.LicenseType", "LicenseTypeRequirementId", "dbo.LicenseTypeRequirement", "LicenseTypeRequirementId", cascadeDelete: true);
            DropColumn("dbo.LicenseTypeRequirement", "LicenseTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LicenseTypeRequirement", "LicenseTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LicenseType", "LicenseTypeRequirementId", "dbo.LicenseTypeRequirement");
            DropIndex("dbo.LicenseType", new[] { "LicenseTypeRequirementId" });
            AlterColumn("dbo.LicenseType", "LicenseTypeRequirementId", c => c.Int());
            RenameColumn(table: "dbo.LicenseType", name: "LicenseTypeRequirementId", newName: "LicenseTypeRequirement_LicenseTypeRequirementId");
            CreateIndex("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId");
            AddForeignKey("dbo.LicenseType", "LicenseTypeRequirement_LicenseTypeRequirementId", "dbo.LicenseTypeRequirement", "LicenseTypeRequirementId");
        }
    }
}
