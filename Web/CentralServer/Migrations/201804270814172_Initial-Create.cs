namespace CentralServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdapterServers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISName = c.String(),
                        Url = c.String(),
                        Root = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BeContracts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Version = c.String(),
                        AdapterServer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdapterServers", t => t.AdapterServer_Id)
                .Index(t => t.AdapterServer_Id);
            
            CreateTable(
                "dbo.Inputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Type = c.String(),
                        Required = c.Boolean(nullable: false),
                        Description = c.String(),
                        BeContract_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BeContracts", t => t.BeContract_Id, cascadeDelete: true)
                .Index(t => t.BeContract_Id);
            
            CreateTable(
                "dbo.Outputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupInputId = c.Int(nullable: false),
                        Key = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        BeContract_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BeContracts", t => t.BeContract_Id, cascadeDelete: true)
                .Index(t => t.BeContract_Id);
            
            CreateTable(
                "dbo.Queries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractId = c.String(maxLength: 128),
                        BeContract_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BeContracts", t => t.ContractId)
                .ForeignKey("dbo.BeContracts", t => t.BeContract_Id, cascadeDelete: true)
                .Index(t => t.ContractId)
                .Index(t => t.BeContract_Id);
            
            CreateTable(
                "dbo.Mappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InputKey = c.String(),
                        LookupInputId = c.Int(nullable: false),
                        LookupInputKey = c.String(),
                        Query_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Queries", t => t.Query_Id, cascadeDelete: true)
                .Index(t => t.Query_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BeContracts", "AdapterServer_Id", "dbo.AdapterServers");
            DropForeignKey("dbo.Queries", "BeContract_Id", "dbo.BeContracts");
            DropForeignKey("dbo.Mappings", "Query_Id", "dbo.Queries");
            DropForeignKey("dbo.Queries", "ContractId", "dbo.BeContracts");
            DropForeignKey("dbo.Outputs", "BeContract_Id", "dbo.BeContracts");
            DropForeignKey("dbo.Inputs", "BeContract_Id", "dbo.BeContracts");
            DropIndex("dbo.Mappings", new[] { "Query_Id" });
            DropIndex("dbo.Queries", new[] { "BeContract_Id" });
            DropIndex("dbo.Queries", new[] { "ContractId" });
            DropIndex("dbo.Outputs", new[] { "BeContract_Id" });
            DropIndex("dbo.Inputs", new[] { "BeContract_Id" });
            DropIndex("dbo.BeContracts", new[] { "AdapterServer_Id" });
            DropTable("dbo.Mappings");
            DropTable("dbo.Queries");
            DropTable("dbo.Outputs");
            DropTable("dbo.Inputs");
            DropTable("dbo.BeContracts");
            DropTable("dbo.AdapterServers");
        }
    }
}
