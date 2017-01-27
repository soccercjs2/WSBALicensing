namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Wrongcollectionmaybe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.License", "DisabilityId", "dbo.Disability");
            DropForeignKey("dbo.License", "EthnicityId", "dbo.Ethnicity");
            DropForeignKey("dbo.License", "FinancialResponsibilityId", "dbo.FinancialResponsibility");
            DropForeignKey("dbo.License", "FirmSizeId", "dbo.FirmSize");
            DropForeignKey("dbo.License", "GenderId", "dbo.Gender");
            DropForeignKey("dbo.License", "JudicialPositionId", "dbo.JudicialPosition");
            DropForeignKey("dbo.License", "ProBonoId", "dbo.ProBono");
            DropForeignKey("dbo.License", "ProfessionalLiabilityInsuranceId", "dbo.ProfessionalLiabilityInsurance");
            DropForeignKey("dbo.License", "SexualOrientationId", "dbo.SexualOrientation");
            DropForeignKey("dbo.License", "TrustAccountId", "dbo.TrustAccount");
            DropIndex("dbo.License", new[] { "FinancialResponsibilityId" });
            DropIndex("dbo.License", new[] { "JudicialPositionId" });
            DropIndex("dbo.License", new[] { "ProBonoId" });
            DropIndex("dbo.License", new[] { "ProfessionalLiabilityInsuranceId" });
            DropIndex("dbo.License", new[] { "TrustAccountId" });
            DropIndex("dbo.License", new[] { "FirmSizeId" });
            DropIndex("dbo.License", new[] { "DisabilityId" });
            DropIndex("dbo.License", new[] { "EthnicityId" });
            DropIndex("dbo.License", new[] { "GenderId" });
            DropIndex("dbo.License", new[] { "SexualOrientationId" });
            RenameColumn(table: "dbo.License", name: "BarNews_BarNewsResponseId", newName: "BarNewsResponseId");
            RenameIndex(table: "dbo.License", name: "IX_BarNews_BarNewsResponseId", newName: "IX_BarNewsResponseId");
            AlterColumn("dbo.License", "FinancialResponsibilityId", c => c.Int());
            AlterColumn("dbo.License", "JudicialPositionId", c => c.Int());
            AlterColumn("dbo.License", "ProBonoId", c => c.Int());
            AlterColumn("dbo.License", "ProfessionalLiabilityInsuranceId", c => c.Int());
            AlterColumn("dbo.License", "TrustAccountId", c => c.Int());
            AlterColumn("dbo.License", "FirmSizeId", c => c.Int());
            AlterColumn("dbo.License", "DisabilityId", c => c.Int());
            AlterColumn("dbo.License", "EthnicityId", c => c.Int());
            AlterColumn("dbo.License", "GenderId", c => c.Int());
            AlterColumn("dbo.License", "SexualOrientationId", c => c.Int());
            CreateIndex("dbo.License", "FinancialResponsibilityId");
            CreateIndex("dbo.License", "JudicialPositionId");
            CreateIndex("dbo.License", "ProBonoId");
            CreateIndex("dbo.License", "ProfessionalLiabilityInsuranceId");
            CreateIndex("dbo.License", "TrustAccountId");
            CreateIndex("dbo.License", "FirmSizeId");
            CreateIndex("dbo.License", "DisabilityId");
            CreateIndex("dbo.License", "EthnicityId");
            CreateIndex("dbo.License", "GenderId");
            CreateIndex("dbo.License", "SexualOrientationId");
            AddForeignKey("dbo.License", "DisabilityId", "dbo.Disability", "DisabilityId");
            AddForeignKey("dbo.License", "EthnicityId", "dbo.Ethnicity", "EthnicityId");
            AddForeignKey("dbo.License", "FinancialResponsibilityId", "dbo.FinancialResponsibility", "FinancialResponsibilityId");
            AddForeignKey("dbo.License", "FirmSizeId", "dbo.FirmSize", "FirmSizeId");
            AddForeignKey("dbo.License", "GenderId", "dbo.Gender", "GenderId");
            AddForeignKey("dbo.License", "JudicialPositionId", "dbo.JudicialPosition", "JudicialPositionId");
            AddForeignKey("dbo.License", "ProBonoId", "dbo.ProBono", "ProBonoId");
            AddForeignKey("dbo.License", "ProfessionalLiabilityInsuranceId", "dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceId");
            AddForeignKey("dbo.License", "SexualOrientationId", "dbo.SexualOrientation", "SexualOrientationId");
            AddForeignKey("dbo.License", "TrustAccountId", "dbo.TrustAccount", "TrustAccountId");
            DropColumn("dbo.License", "BarNewsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.License", "BarNewsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.License", "TrustAccountId", "dbo.TrustAccount");
            DropForeignKey("dbo.License", "SexualOrientationId", "dbo.SexualOrientation");
            DropForeignKey("dbo.License", "ProfessionalLiabilityInsuranceId", "dbo.ProfessionalLiabilityInsurance");
            DropForeignKey("dbo.License", "ProBonoId", "dbo.ProBono");
            DropForeignKey("dbo.License", "JudicialPositionId", "dbo.JudicialPosition");
            DropForeignKey("dbo.License", "GenderId", "dbo.Gender");
            DropForeignKey("dbo.License", "FirmSizeId", "dbo.FirmSize");
            DropForeignKey("dbo.License", "FinancialResponsibilityId", "dbo.FinancialResponsibility");
            DropForeignKey("dbo.License", "EthnicityId", "dbo.Ethnicity");
            DropForeignKey("dbo.License", "DisabilityId", "dbo.Disability");
            DropIndex("dbo.License", new[] { "SexualOrientationId" });
            DropIndex("dbo.License", new[] { "GenderId" });
            DropIndex("dbo.License", new[] { "EthnicityId" });
            DropIndex("dbo.License", new[] { "DisabilityId" });
            DropIndex("dbo.License", new[] { "FirmSizeId" });
            DropIndex("dbo.License", new[] { "TrustAccountId" });
            DropIndex("dbo.License", new[] { "ProfessionalLiabilityInsuranceId" });
            DropIndex("dbo.License", new[] { "ProBonoId" });
            DropIndex("dbo.License", new[] { "JudicialPositionId" });
            DropIndex("dbo.License", new[] { "FinancialResponsibilityId" });
            AlterColumn("dbo.License", "SexualOrientationId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "GenderId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "EthnicityId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "DisabilityId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "FirmSizeId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "TrustAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "ProfessionalLiabilityInsuranceId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "ProBonoId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "JudicialPositionId", c => c.Int(nullable: false));
            AlterColumn("dbo.License", "FinancialResponsibilityId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.License", name: "IX_BarNewsResponseId", newName: "IX_BarNews_BarNewsResponseId");
            RenameColumn(table: "dbo.License", name: "BarNewsResponseId", newName: "BarNews_BarNewsResponseId");
            CreateIndex("dbo.License", "SexualOrientationId");
            CreateIndex("dbo.License", "GenderId");
            CreateIndex("dbo.License", "EthnicityId");
            CreateIndex("dbo.License", "DisabilityId");
            CreateIndex("dbo.License", "FirmSizeId");
            CreateIndex("dbo.License", "TrustAccountId");
            CreateIndex("dbo.License", "ProfessionalLiabilityInsuranceId");
            CreateIndex("dbo.License", "ProBonoId");
            CreateIndex("dbo.License", "JudicialPositionId");
            CreateIndex("dbo.License", "FinancialResponsibilityId");
            AddForeignKey("dbo.License", "TrustAccountId", "dbo.TrustAccount", "TrustAccountId", cascadeDelete: true);
            AddForeignKey("dbo.License", "SexualOrientationId", "dbo.SexualOrientation", "SexualOrientationId", cascadeDelete: true);
            AddForeignKey("dbo.License", "ProfessionalLiabilityInsuranceId", "dbo.ProfessionalLiabilityInsurance", "ProfessionalLiabilityInsuranceId", cascadeDelete: true);
            AddForeignKey("dbo.License", "ProBonoId", "dbo.ProBono", "ProBonoId", cascadeDelete: true);
            AddForeignKey("dbo.License", "JudicialPositionId", "dbo.JudicialPosition", "JudicialPositionId", cascadeDelete: true);
            AddForeignKey("dbo.License", "GenderId", "dbo.Gender", "GenderId", cascadeDelete: true);
            AddForeignKey("dbo.License", "FirmSizeId", "dbo.FirmSize", "FirmSizeId", cascadeDelete: true);
            AddForeignKey("dbo.License", "FinancialResponsibilityId", "dbo.FinancialResponsibility", "FinancialResponsibilityId", cascadeDelete: true);
            AddForeignKey("dbo.License", "EthnicityId", "dbo.Ethnicity", "EthnicityId", cascadeDelete: true);
            AddForeignKey("dbo.License", "DisabilityId", "dbo.Disability", "DisabilityId", cascadeDelete: true);
        }
    }
}
