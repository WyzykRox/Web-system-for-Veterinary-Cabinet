namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Animal", "grafting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animal", "grafting", c => c.DateTime(nullable: false));
        }
    }
}
