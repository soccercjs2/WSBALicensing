namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAmsSequenceNumbertoProfessionalLiabilityInsurance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfessionalLiabilityInsurance", "AmsSequenceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfessionalLiabilityInsurance", "AmsSequenceNumber");
        }
    }
}
