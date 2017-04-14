namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makinglastamsupdatenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.License", "LastAmsUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.License", "LastAmsUpdate", c => c.DateTime(nullable: false));
        }
    }
}
