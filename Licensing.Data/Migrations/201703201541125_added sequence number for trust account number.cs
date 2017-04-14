namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsequencenumberfortrustaccountnumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrustAccountNumber", "AmsSequenceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrustAccountNumber", "AmsSequenceNumber");
        }
    }
}
