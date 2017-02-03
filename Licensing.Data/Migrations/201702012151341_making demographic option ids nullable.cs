namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingdemographicoptionidsnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Disability", "DisabilityOptionId", "dbo.DisabilityOption");
            DropForeignKey("dbo.Ethnicity", "EthnicityOptionId", "dbo.EthnicityOption");
            DropForeignKey("dbo.Gender", "GenderOptionId", "dbo.GenderOption");
            DropForeignKey("dbo.SexualOrientation", "SexualOrientationOptionId", "dbo.SexualOrientationOption");
            DropIndex("dbo.Disability", new[] { "DisabilityOptionId" });
            DropIndex("dbo.Ethnicity", new[] { "EthnicityOptionId" });
            DropIndex("dbo.Gender", new[] { "GenderOptionId" });
            DropIndex("dbo.SexualOrientation", new[] { "SexualOrientationOptionId" });
            AlterColumn("dbo.Disability", "DisabilityOptionId", c => c.Int());
            AlterColumn("dbo.Ethnicity", "EthnicityOptionId", c => c.Int());
            AlterColumn("dbo.Gender", "GenderOptionId", c => c.Int());
            AlterColumn("dbo.SexualOrientation", "SexualOrientationOptionId", c => c.Int());
            CreateIndex("dbo.Disability", "DisabilityOptionId");
            CreateIndex("dbo.Ethnicity", "EthnicityOptionId");
            CreateIndex("dbo.Gender", "GenderOptionId");
            CreateIndex("dbo.SexualOrientation", "SexualOrientationOptionId");
            AddForeignKey("dbo.Disability", "DisabilityOptionId", "dbo.DisabilityOption", "DisabilityOptionId");
            AddForeignKey("dbo.Ethnicity", "EthnicityOptionId", "dbo.EthnicityOption", "EthnicityOptionId");
            AddForeignKey("dbo.Gender", "GenderOptionId", "dbo.GenderOption", "GenderOptionId");
            AddForeignKey("dbo.SexualOrientation", "SexualOrientationOptionId", "dbo.SexualOrientationOption", "SexualOrientationOptionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SexualOrientation", "SexualOrientationOptionId", "dbo.SexualOrientationOption");
            DropForeignKey("dbo.Gender", "GenderOptionId", "dbo.GenderOption");
            DropForeignKey("dbo.Ethnicity", "EthnicityOptionId", "dbo.EthnicityOption");
            DropForeignKey("dbo.Disability", "DisabilityOptionId", "dbo.DisabilityOption");
            DropIndex("dbo.SexualOrientation", new[] { "SexualOrientationOptionId" });
            DropIndex("dbo.Gender", new[] { "GenderOptionId" });
            DropIndex("dbo.Ethnicity", new[] { "EthnicityOptionId" });
            DropIndex("dbo.Disability", new[] { "DisabilityOptionId" });
            AlterColumn("dbo.SexualOrientation", "SexualOrientationOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Gender", "GenderOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Ethnicity", "EthnicityOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Disability", "DisabilityOptionId", c => c.Int(nullable: false));
            CreateIndex("dbo.SexualOrientation", "SexualOrientationOptionId");
            CreateIndex("dbo.Gender", "GenderOptionId");
            CreateIndex("dbo.Ethnicity", "EthnicityOptionId");
            CreateIndex("dbo.Disability", "DisabilityOptionId");
            AddForeignKey("dbo.SexualOrientation", "SexualOrientationOptionId", "dbo.SexualOrientationOption", "SexualOrientationOptionId", cascadeDelete: true);
            AddForeignKey("dbo.Gender", "GenderOptionId", "dbo.GenderOption", "GenderOptionId", cascadeDelete: true);
            AddForeignKey("dbo.Ethnicity", "EthnicityOptionId", "dbo.EthnicityOption", "EthnicityOptionId", cascadeDelete: true);
            AddForeignKey("dbo.Disability", "DisabilityOptionId", "dbo.DisabilityOption", "DisabilityOptionId", cascadeDelete: true);
        }
    }
}
