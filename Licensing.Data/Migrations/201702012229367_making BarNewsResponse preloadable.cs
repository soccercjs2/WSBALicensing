namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingBarNewsResponsepreloadable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BarNewsResponse", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BarNewsResponse", "Confirmed");
        }
    }
}
