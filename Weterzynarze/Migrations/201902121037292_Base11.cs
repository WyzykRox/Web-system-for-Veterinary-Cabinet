namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Race", "AnimalID", "dbo.Animal");
            DropIndex("dbo.Race", new[] { "AnimalID" });
            AddColumn("dbo.Animal", "Races_ID", c => c.Int());
            CreateIndex("dbo.Animal", "Races_ID");
            AddForeignKey("dbo.Animal", "Races_ID", "dbo.Race", "ID");
            DropColumn("dbo.Race", "AnimalID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Race", "AnimalID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Animal", "Races_ID", "dbo.Race");
            DropIndex("dbo.Animal", new[] { "Races_ID" });
            DropColumn("dbo.Animal", "Races_ID");
            CreateIndex("dbo.Race", "AnimalID");
            AddForeignKey("dbo.Race", "AnimalID", "dbo.Animal", "ID", cascadeDelete: true);
        }
    }
}
