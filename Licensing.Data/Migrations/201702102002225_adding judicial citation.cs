namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingjudicialcitation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JudicialPosition", "Citation", c => c.String());
            AddColumn("dbo.JudicialPositionOption", "CitationRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JudicialPositionOption", "CitationRequired");
            DropColumn("dbo.JudicialPosition", "Citation");
        }
    }
}
