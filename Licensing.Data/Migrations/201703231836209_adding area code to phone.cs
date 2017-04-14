namespace Licensing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingareacodetophone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhoneNumber", "AreaCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhoneNumber", "AreaCode");
        }
    }
}
