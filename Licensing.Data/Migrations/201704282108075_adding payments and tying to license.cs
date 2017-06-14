namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingpaymentsandtyingtolicense : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        LicenseId = c.Int(nullable: false),
                        AmsCode = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .Index(t => t.LicenseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "LicenseId", "dbo.License");
            DropIndex("dbo.Payment", new[] { "LicenseId" });
            DropTable("dbo.Payment");
        }
    }
}
