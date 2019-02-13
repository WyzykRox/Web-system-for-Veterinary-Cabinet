namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Animal", "Races_ID", "dbo.Race");
            DropIndex("dbo.Animal", new[] { "Races_ID" });
            AddColumn("dbo.Animal", "Rasa", c => c.Int(nullable: false));
            AddColumn("dbo.Race", "Rasa_ID", c => c.Int());
            CreateIndex("dbo.Race", "Rasa_ID");
            AddForeignKey("dbo.Race", "Rasa_ID", "dbo.Animal", "ID");
            DropColumn("dbo.Animal", "Races_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animal", "Races_ID", c => c.Int());
            DropForeignKey("dbo.Race", "Rasa_ID", "dbo.Animal");
            DropIndex("dbo.Race", new[] { "Rasa_ID" });
            DropColumn("dbo.Race", "Rasa_ID");
            DropColumn("dbo.Animal", "Rasa");
            CreateIndex("dbo.Animal", "Races_ID");
            AddForeignKey("dbo.Animal", "Races_ID", "dbo.Race", "ID");
        }
    }
}
