namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LicensingtoLicense : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.License", "LicensingPeriodId", "dbo.LicensingPeriod");
            DropIndex("dbo.License", new[] { "LicensingPeriodId" });
            CreateTable(
                "dbo.LicensePeriod",
                c => new
                    {
                        LicensePeriodId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        LateFeeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LicensePeriodId);
            
            AddColumn("dbo.License", "LicensePeriodId", c => c.Int());
            CreateIndex("dbo.License", "LicensePeriodId");
            AddForeignKey("dbo.License", "LicensePeriodId", "dbo.LicensePeriod", "LicensePeriodId");
            DropColumn("dbo.License", "LicensingPeriodId");
            DropTable("dbo.LicensingPeriod");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LicensingPeriod",
                c => new
                    {
                        LicensingPeriodId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        LateFeeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LicensingPeriodId);
            
            AddColumn("dbo.License", "LicensingPeriodId", c => c.Int());
            DropForeignKey("dbo.License", "LicensePeriodId", "dbo.LicensePeriod");
            DropIndex("dbo.License", new[] { "LicensePeriodId" });
            DropColumn("dbo.License", "LicensePeriodId");
            DropTable("dbo.LicensePeriod");
            CreateIndex("dbo.License", "LicensingPeriodId");
            AddForeignKey("dbo.License", "LicensingPeriodId", "dbo.LicensingPeriod", "LicensingPeriodId");
        }
    }
}
