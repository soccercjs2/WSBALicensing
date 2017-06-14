namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingadmissiondatestocustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "EarliestAdmissionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customer", "WaAdmissionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "WaAdmissionDate");
            DropColumn("dbo.Customer", "EarliestAdmissionDate");
        }
    }
}
