namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingorders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payment", "LicenseId", "dbo.License");
            DropIndex("dbo.Payment", new[] { "LicenseId" });
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        AmsOrderNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            AddColumn("dbo.License", "LicensingOrderId", c => c.Int());
            AddColumn("dbo.License", "SectionOrderId", c => c.Int());
            AddColumn("dbo.Payment", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.License", "LicensingOrderId");
            CreateIndex("dbo.License", "SectionOrderId");
            CreateIndex("dbo.Payment", "OrderId");
            AddForeignKey("dbo.Payment", "OrderId", "dbo.Order", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.License", "LicensingOrderId", "dbo.Order", "OrderId");
            AddForeignKey("dbo.License", "SectionOrderId", "dbo.Order", "OrderId");
            DropColumn("dbo.Payment", "LicenseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payment", "LicenseId", c => c.Int(nullable: false));
            DropForeignKey("dbo.License", "SectionOrderId", "dbo.Order");
            DropForeignKey("dbo.License", "LicensingOrderId", "dbo.Order");
            DropForeignKey("dbo.Payment", "OrderId", "dbo.Order");
            DropIndex("dbo.Payment", new[] { "OrderId" });
            DropIndex("dbo.License", new[] { "SectionOrderId" });
            DropIndex("dbo.License", new[] { "LicensingOrderId" });
            DropColumn("dbo.Payment", "OrderId");
            DropColumn("dbo.License", "SectionOrderId");
            DropColumn("dbo.License", "LicensingOrderId");
            DropTable("dbo.Order");
            CreateIndex("dbo.Payment", "LicenseId");
            AddForeignKey("dbo.Payment", "LicenseId", "dbo.License", "LicenseId", cascadeDelete: true);
        }
    }
}
