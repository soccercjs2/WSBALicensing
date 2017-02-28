namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingpracticeareas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PracticeArea",
                c => new
                    {
                        PracticeAreaId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        PracticeAreaOptionId = c.Int(),
                    })
                .PrimaryKey(t => t.PracticeAreaId)
                .ForeignKey("dbo.PracticeAreaOption", t => t.PracticeAreaOptionId)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.PracticeAreaOptionId);
            
            CreateTable(
                "dbo.PracticeAreaOption",
                c => new
                    {
                        PracticeAreaOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.PracticeAreaOptionId);
            
            AddColumn("dbo.License", "PracticeAreasConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.LicenseType", "PracticeAreas", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PracticeArea", "LicenseId", "dbo.License");
            DropForeignKey("dbo.PracticeArea", "PracticeAreaOptionId", "dbo.PracticeAreaOption");
            DropIndex("dbo.PracticeArea", new[] { "PracticeAreaOptionId" });
            DropIndex("dbo.PracticeArea", new[] { "LicenseId" });
            DropColumn("dbo.LicenseType", "PracticeAreas");
            DropColumn("dbo.License", "PracticeAreasConfirmed");
            DropTable("dbo.PracticeAreaOption");
            DropTable("dbo.PracticeArea");
        }
    }
}
