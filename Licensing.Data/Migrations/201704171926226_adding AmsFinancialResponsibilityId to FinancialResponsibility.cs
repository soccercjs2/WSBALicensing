namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAmsFinancialResponsibilityIdtoFinancialResponsibility : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinancialResponsibility", "AmsFinancialResponsibilityId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FinancialResponsibility", "AmsFinancialResponsibilityId");
        }
    }
}
