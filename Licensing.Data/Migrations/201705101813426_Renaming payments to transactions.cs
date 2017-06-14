namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamingpaymentstotransactions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payment", "OrderId", "dbo.Order");
            DropIndex("dbo.Payment", new[] { "OrderId" });
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        AmsTransactionId = c.Int(nullable: false),
                        AmsCode = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            DropTable("dbo.Payment");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        AmsCode = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PaymentId);
            
            DropForeignKey("dbo.Transaction", "OrderId", "dbo.Order");
            DropIndex("dbo.Transaction", new[] { "OrderId" });
            DropTable("dbo.Transaction");
            CreateIndex("dbo.Payment", "OrderId");
            AddForeignKey("dbo.Payment", "OrderId", "dbo.Order", "OrderId", cascadeDelete: true);
        }
    }
}
