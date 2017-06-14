namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingkellerdeductionandhardshipexemptiontorequirementslicensefeeexempttolicensekellerdeductionpercentagetolicensetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.License", "LicenseFeeExempt", c => c.Boolean(nullable: false));
            AddColumn("dbo.LicenseType", "KellerDeductionPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LicenseTypeRequirement", "HardshipExemption", c => c.Int(nullable: false));
            AddColumn("dbo.LicenseTypeRequirement", "KellerDeduction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LicenseTypeRequirement", "KellerDeduction");
            DropColumn("dbo.LicenseTypeRequirement", "HardshipExemption");
            DropColumn("dbo.LicenseType", "KellerDeductionPercentage");
            DropColumn("dbo.License", "LicenseFeeExempt");
        }
    }
}
