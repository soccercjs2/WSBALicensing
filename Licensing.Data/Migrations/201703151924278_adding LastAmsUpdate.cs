namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingLastAmsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "LastAmsUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.License", "LastAmsUpdate");
        }
    }
}
