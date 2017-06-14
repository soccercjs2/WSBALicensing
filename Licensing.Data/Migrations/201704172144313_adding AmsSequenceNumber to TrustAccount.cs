namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAmsSequenceNumbertoTrustAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrustAccount", "AmsSequenceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrustAccount", "AmsSequenceNumber");
        }
    }
}
