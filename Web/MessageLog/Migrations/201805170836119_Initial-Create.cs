namespace MessageLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessInfoHashes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hash = c.String(),
                        TransactionAddress = c.String(),
                        AccessInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccessInfoes", t => t.AccessInfo_Id)
                .Index(t => t.AccessInfo_Id);
            
            CreateTable(
                "dbo.AccessInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NRID = c.String(),
                        ContractId = c.String(),
                        Name = c.String(),
                        Justification = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deter = c.Int(nullable: false),
                        ContractId = c.String(),
                        UseType = c.String(),
                        Response = c.Boolean(nullable: false),
                        UserType = c.String(),
                        UserName = c.String(),
                        Message = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccessInfoHashes", "AccessInfo_Id", "dbo.AccessInfoes");
            DropIndex("dbo.AccessInfoHashes", new[] { "AccessInfo_Id" });
            DropTable("dbo.Logs");
            DropTable("dbo.AccessInfoes");
            DropTable("dbo.AccessInfoHashes");
        }
    }
}
