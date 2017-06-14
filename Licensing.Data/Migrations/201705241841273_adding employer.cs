namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingemployer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employer",
                c => new
                    {
                        EmployerId = c.Int(nullable: false, identity: true),
                        MasterCustomerId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.EmployerId);
            
            AddColumn("dbo.License", "EmployerId", c => c.Int());
            CreateIndex("dbo.License", "EmployerId");
            AddForeignKey("dbo.License", "EmployerId", "dbo.Employer", "EmployerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.License", "EmployerId", "dbo.Employer");
            DropIndex("dbo.License", new[] { "EmployerId" });
            DropColumn("dbo.License", "EmployerId");
            DropTable("dbo.Employer");
        }
    }
}
