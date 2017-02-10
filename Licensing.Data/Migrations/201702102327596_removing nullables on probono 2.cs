namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingnullablesonprobono2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProBono", "Anonymous", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProBono", "Anonymous", c => c.Boolean());
        }
    }
}
