namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animal", "Rasa", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animal", "Rasa", c => c.Int(nullable: false));
        }
    }
}
