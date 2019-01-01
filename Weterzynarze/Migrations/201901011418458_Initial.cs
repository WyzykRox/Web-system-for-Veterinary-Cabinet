namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Colour = c.String(),
                        Sex = c.String(),
                        Breed = c.String(),
                        DistinuishingMarks = c.String(),
                        ChipId = c.Int(nullable: false),
                        Picture = c.String(),
                        Created = c.DateTime(nullable: false),
                        Owner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profile", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.HealthCard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Treatment = c.String(),
                        AnimalID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Animal", t => t.AnimalID, cascadeDelete: true)
                .Index(t => t.AnimalID);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Visit",
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
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PetId = c.Int(nullable: false),
                        PetImage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Animal", t => t.PetImage_ID)
                .Index(t => t.PetImage_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Image", "PetImage_ID", "dbo.Animal");
            DropForeignKey("dbo.Visit", "AnimalID", "dbo.Animal");
            DropForeignKey("dbo.Visit", "User_ID", "dbo.Profile");
            DropForeignKey("dbo.Animal", "Owner_ID", "dbo.Profile");
            DropForeignKey("dbo.HealthCard", "AnimalID", "dbo.Animal");
            DropIndex("dbo.Image", new[] { "PetImage_ID" });
            DropIndex("dbo.Visit", new[] { "User_ID" });
            DropIndex("dbo.Visit", new[] { "AnimalID" });
            DropIndex("dbo.HealthCard", new[] { "AnimalID" });
            DropIndex("dbo.Animal", new[] { "Owner_ID" });
            DropTable("dbo.Image");
            DropTable("dbo.Visit");
            DropTable("dbo.Profile");
            DropTable("dbo.HealthCard");
            DropTable("dbo.Animal");
        }
    }
}
