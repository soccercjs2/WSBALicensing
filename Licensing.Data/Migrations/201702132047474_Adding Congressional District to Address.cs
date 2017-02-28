namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCongressionalDistricttoAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "CongressionalDistrict", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "CongressionalDistrict");
        }
    }
}
