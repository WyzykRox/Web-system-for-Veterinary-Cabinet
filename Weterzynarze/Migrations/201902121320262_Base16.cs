namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Image", "PetImage_ID", "dbo.Animal");
            DropIndex("dbo.Image", new[] { "PetImage_ID" });
            AddColumn("dbo.Image", "CardID", c => c.Int(nullable: false));
            AddColumn("dbo.Image", "cardImage_ID", c => c.Int());
            CreateIndex("dbo.Image", "cardImage_ID");
            AddForeignKey("dbo.Image", "cardImage_ID", "dbo.HealthCard", "ID");
            DropColumn("dbo.Image", "PetId");
            DropColumn("dbo.Image", "PetImage_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Image", "PetImage_ID", c => c.Int());
            AddColumn("dbo.Image", "PetId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Image", "cardImage_ID", "dbo.HealthCard");
            DropIndex("dbo.Image", new[] { "cardImage_ID" });
            DropColumn("dbo.Image", "cardImage_ID");
            DropColumn("dbo.Image", "CardID");
            CreateIndex("dbo.Image", "PetImage_ID");
            AddForeignKey("dbo.Image", "PetImage_ID", "dbo.Animal", "ID");
        }
    }
}
