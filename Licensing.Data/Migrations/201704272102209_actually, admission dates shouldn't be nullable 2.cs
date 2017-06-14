namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actuallyadmissiondatesshouldntbenullable2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "EarliestAdmissionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customer", "WaAdmissionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "WaAdmissionDate", c => c.DateTime());
            AlterColumn("dbo.Customer", "EarliestAdmissionDate", c => c.DateTime());
        }
    }
}
