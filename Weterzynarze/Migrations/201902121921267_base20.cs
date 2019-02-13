namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class base20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilesHealthCard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KartaID = c.Int(nullable: false),
                        Src = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HealthCard", t => t.KartaID, cascadeDelete: true)
                .Index(t => t.KartaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilesHealthCard", "KartaID", "dbo.HealthCard");
            DropIndex("dbo.FilesHealthCard", new[] { "KartaID" });
            DropTable("dbo.FilesHealthCard");
        }
    }
}
