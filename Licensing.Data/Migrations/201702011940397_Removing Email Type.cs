namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingEmailType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Email", "EmailTypeId", "dbo.EmailType");
            DropIndex("dbo.Email", new[] { "EmailTypeId" });
            DropColumn("dbo.Email", "EmailTypeId");
            DropTable("dbo.EmailType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmailType",
                c => new
                    {
                        EmailTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AmsCode = c.String(),
                    })
                .PrimaryKey(t => t.EmailTypeId);
            
            AddColumn("dbo.Email", "EmailTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Email", "EmailTypeId");
            AddForeignKey("dbo.Email", "EmailTypeId", "dbo.EmailType", "EmailTypeId", cascadeDelete: true);
        }
    }
}
