namespace Weterzynarze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HealthCard", "usluga_ID", "dbo.Service");
            DropIndex("dbo.HealthCard", new[] { "usluga_ID" });
            RenameColumn(table: "dbo.HealthCard", name: "usluga_ID", newName: "uslugaID");
            AlterColumn("dbo.HealthCard", "uslugaID", c => c.Int(nullable: false));
            CreateIndex("dbo.HealthCard", "uslugaID");
            AddForeignKey("dbo.HealthCard", "uslugaID", "dbo.Service", "ID", cascadeDelete: true);
            DropColumn("dbo.HealthCard", "servicesID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HealthCard", "servicesID", c => c.Int(nullable: false));
            DropForeignKey("dbo.HealthCard", "uslugaID", "dbo.Service");
            DropIndex("dbo.HealthCard", new[] { "uslugaID" });
            AlterColumn("dbo.HealthCard", "uslugaID", c => c.Int());
            RenameColumn(table: "dbo.HealthCard", name: "uslugaID", newName: "usluga_ID");
            CreateIndex("dbo.HealthCard", "usluga_ID");
            AddForeignKey("dbo.HealthCard", "usluga_ID", "dbo.Service", "ID");
        }
    }
}
