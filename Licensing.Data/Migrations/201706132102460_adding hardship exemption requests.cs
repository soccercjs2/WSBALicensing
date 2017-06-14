namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinghardshipexemptionrequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HardshipExemptionRequest",
                c => new
                    {
                        HardshipExemptionRequestId = c.Int(nullable: false, identity: true),
                        Income = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FamilySize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HardshipExemptionRequestId);
            
            AddColumn("dbo.License", "HardshipExemptionRequestId", c => c.Int());
            CreateIndex("dbo.License", "HardshipExemptionRequestId");
            AddForeignKey("dbo.License", "HardshipExemptionRequestId", "dbo.HardshipExemptionRequest", "HardshipExemptionRequestId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.License", "HardshipExemptionRequestId", "dbo.HardshipExemptionRequest");
            DropIndex("dbo.License", new[] { "HardshipExemptionRequestId" });
            DropColumn("dbo.License", "HardshipExemptionRequestId");
            DropTable("dbo.HardshipExemptionRequest");
        }
    }
}
