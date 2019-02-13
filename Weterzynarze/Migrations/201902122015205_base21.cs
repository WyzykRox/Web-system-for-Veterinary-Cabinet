namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class base21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilesHealthCard", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilesHealthCard", "Name");
        }
    }
}
