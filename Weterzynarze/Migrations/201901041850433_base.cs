namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _base : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistryVisit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VisitDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        AnimalID = c.Int(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profile", t => t.User_ID)
                .ForeignKey("dbo.Animal", t => t.AnimalID, cascadeDelete: true)
                .Index(t => t.AnimalID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistryVisit", "AnimalID", "dbo.Animal");
            DropForeignKey("dbo.HistryVisit", "User_ID", "dbo.Profile");
            DropIndex("dbo.HistryVisit", new[] { "User_ID" });
            DropIndex("dbo.HistryVisit", new[] { "AnimalID" });
            DropTable("dbo.HistryVisit");
        }
    }
}
