namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Synopsis = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profile", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "UserID", "dbo.Profile");
            DropIndex("dbo.News", new[] { "UserID" });
            DropTable("dbo.News");
        }
    }
}
