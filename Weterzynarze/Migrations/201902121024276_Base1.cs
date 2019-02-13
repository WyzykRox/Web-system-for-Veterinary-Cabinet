namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "PhoneNumber", c => c.String());
            DropColumn("dbo.Profile", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profile", "Phone", c => c.String());
            DropColumn("dbo.Profile", "PhoneNumber");
        }
    }
}
