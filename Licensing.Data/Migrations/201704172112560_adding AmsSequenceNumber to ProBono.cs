namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAmsSequenceNumbertoProBono : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProBono", "AmsSequenceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProBono", "AmsSequenceNumber");
        }
    }
}
