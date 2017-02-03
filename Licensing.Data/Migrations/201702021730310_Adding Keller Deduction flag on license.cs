namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingKellerDeductionflagonlicense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "KellerDeduction", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.License", "KellerDeduction");
        }
    }
}
