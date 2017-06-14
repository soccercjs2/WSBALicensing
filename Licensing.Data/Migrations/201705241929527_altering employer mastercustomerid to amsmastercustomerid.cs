namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteringemployermastercustomeridtoamsmastercustomerid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employer", "AmsMasterCustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Employer", "MasterCustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employer", "MasterCustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Employer", "AmsMasterCustomerId");
        }
    }
}
