namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Race", "Rasa_ID", "dbo.Animal");
            DropIndex("dbo.Race", new[] { "Rasa_ID" });
            AddColumn("dbo.Animal", "RaceID", c => c.Int(nullable: false));
            CreateIndex("dbo.Animal", "RaceID");
            AddForeignKey("dbo.Animal", "RaceID", "dbo.Race", "ID", cascadeDelete: true);
            DropColumn("dbo.Animal", "Rasa");
            DropColumn("dbo.Race", "Rasa_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Race", "Rasa_ID", c => c.Int());
            AddColumn("dbo.Animal", "Rasa", c => c.String());
            DropForeignKey("dbo.Animal", "RaceID", "dbo.Race");
            DropIndex("dbo.Animal", new[] { "RaceID" });
            DropColumn("dbo.Animal", "RaceID");
            CreateIndex("dbo.Race", "Rasa_ID");
            AddForeignKey("dbo.Race", "Rasa_ID", "dbo.Animal", "ID");
        }
    }
}
