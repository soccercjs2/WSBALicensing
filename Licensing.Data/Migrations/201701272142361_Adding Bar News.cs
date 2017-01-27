namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBarNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BarNewsResponse",
                c => new
                    {
                        BarNewsResponseId = c.Int(nullable: false, identity: true),
                        Response = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BarNewsResponseId);
            
            AddColumn("dbo.License", "BarNews_BarNewsResponseId", c => c.Int());
            CreateIndex("dbo.License", "BarNews_BarNewsResponseId");
            AddForeignKey("dbo.License", "BarNews_BarNewsResponseId", "dbo.BarNewsResponse", "BarNewsResponseId");
            DropColumn("dbo.License", "BarNews");
        }
        
        public override void Down()
        {
            AddColumn("dbo.License", "BarNews", c => c.Boolean());
            DropForeignKey("dbo.License", "BarNews_BarNewsResponseId", "dbo.BarNewsResponse");
            DropIndex("dbo.License", new[] { "BarNews_BarNewsResponseId" });
            DropColumn("dbo.License", "BarNews_BarNewsResponseId");
            DropTable("dbo.BarNewsResponse");
        }
    }
}
