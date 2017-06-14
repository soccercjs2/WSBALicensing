namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingadmissiondatesnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "EarliestAdmissionDate", c => c.DateTime());
            AlterColumn("dbo.Customer", "WaAdmissionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "WaAdmissionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customer", "EarliestAdmissionDate", c => c.DateTime(nullable: false));
        }
    }
}
