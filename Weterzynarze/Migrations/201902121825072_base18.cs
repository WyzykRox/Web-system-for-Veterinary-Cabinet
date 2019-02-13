namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class base18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Image", "cardImage_ID", "dbo.HealthCard");
            DropIndex("dbo.Image", new[] { "cardImage_ID" });
            DropTable("dbo.Image");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CardID = c.Int(nullable: false),
                        cardImage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Image", "cardImage_ID");
            AddForeignKey("dbo.Image", "cardImage_ID", "dbo.HealthCard", "ID");
        }
    }
}
