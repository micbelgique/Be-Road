namespace Contracts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractNamesFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AdapterServer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdapterServers", t => t.AdapterServer_Id)
                .Index(t => t.AdapterServer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractNames", "AdapterServer_Id", "dbo.AdapterServers");
            DropIndex("dbo.ContractNames", new[] { "AdapterServer_Id" });
            DropTable("dbo.ContractNames");
        }
    }
}
