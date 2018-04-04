namespace Proxy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BeContracts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Version = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        ContractId = c.String(maxLength: 128),
                        Key = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        BeContract_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BeContracts", t => t.ContractId)
                .ForeignKey("dbo.BeContracts", t => t.BeContract_Id, cascadeDelete: true)
                .Index(t => t.ContractId)
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
                        ContractKey = c.String(),
                        Contract_Id = c.String(maxLength: 128),
                        Query_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BeContracts", t => t.Contract_Id)
                .ForeignKey("dbo.Queries", t => t.Query_Id, cascadeDelete: true)
                .Index(t => t.Contract_Id)
                .Index(t => t.Query_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queries", "BeContract_Id", "dbo.BeContracts");
            DropForeignKey("dbo.Mappings", "Query_Id", "dbo.Queries");
            DropForeignKey("dbo.Mappings", "Contract_Id", "dbo.BeContracts");
            DropForeignKey("dbo.Queries", "ContractId", "dbo.BeContracts");
            DropForeignKey("dbo.Outputs", "BeContract_Id", "dbo.BeContracts");
            DropForeignKey("dbo.Outputs", "ContractId", "dbo.BeContracts");
            DropForeignKey("dbo.Inputs", "BeContract_Id", "dbo.BeContracts");
            DropIndex("dbo.Mappings", new[] { "Query_Id" });
            DropIndex("dbo.Mappings", new[] { "Contract_Id" });
            DropIndex("dbo.Queries", new[] { "BeContract_Id" });
            DropIndex("dbo.Queries", new[] { "ContractId" });
            DropIndex("dbo.Outputs", new[] { "BeContract_Id" });
            DropIndex("dbo.Outputs", new[] { "ContractId" });
            DropIndex("dbo.Inputs", new[] { "BeContract_Id" });
            DropTable("dbo.Mappings");
            DropTable("dbo.Queries");
            DropTable("dbo.Outputs");
            DropTable("dbo.Inputs");
            DropTable("dbo.BeContracts");
        }
    }
}
