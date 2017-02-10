namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingnullablesonprobono : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProBono", "ProvidesService", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProBono", "FreeServiceHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ProBono", "LimitedFeeServiceHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProBono", "LimitedFeeServiceHours", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ProBono", "FreeServiceHours", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ProBono", "ProvidesService", c => c.Boolean());
        }
    }
}
