namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.HealthCard", "servicesID", c => c.Int(nullable: false));
            AddColumn("dbo.HealthCard", "usluga_ID", c => c.Int());
            CreateIndex("dbo.HealthCard", "usluga_ID");
            AddForeignKey("dbo.HealthCard", "usluga_ID", "dbo.Service", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HealthCard", "usluga_ID", "dbo.Service");
            DropIndex("dbo.HealthCard", new[] { "usluga_ID" });
            DropColumn("dbo.HealthCard", "usluga_ID");
            DropColumn("dbo.HealthCard", "servicesID");
            DropTable("dbo.Service");
        }
    }
}
