namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingBarNewsfieldonLicence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "BarNewsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.License", "BarNewsId");
        }
    }
}
