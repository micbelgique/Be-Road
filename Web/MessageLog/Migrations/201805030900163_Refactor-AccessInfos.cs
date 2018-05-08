namespace MessageLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorAccessInfos : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AccessInfoes");
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
            
            AddColumn("dbo.AccessInfoes", "NRID", c => c.String());
            AddColumn("dbo.AccessInfoes", "ContractId", c => c.String());
            AddColumn("dbo.AccessInfoes", "Name", c => c.String());
            AddColumn("dbo.AccessInfoes", "Justification", c => c.String());
            AddColumn("dbo.AccessInfoes", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AccessInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.AccessInfoes", "Id");
            DropColumn("dbo.AccessInfoes", "Hash");
            DropColumn("dbo.AccessInfoes", "TransactionAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccessInfoes", "TransactionAddress", c => c.String());
            AddColumn("dbo.AccessInfoes", "Hash", c => c.String());
            DropForeignKey("dbo.AccessInfoHashes", "AccessInfo_Id", "dbo.AccessInfoes");
            DropIndex("dbo.AccessInfoHashes", new[] { "AccessInfo_Id" });
            DropPrimaryKey("dbo.AccessInfoes");
            AlterColumn("dbo.AccessInfoes", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.AccessInfoes", "Date");
            DropColumn("dbo.AccessInfoes", "Justification");
            DropColumn("dbo.AccessInfoes", "Name");
            DropColumn("dbo.AccessInfoes", "ContractId");
            DropColumn("dbo.AccessInfoes", "NRID");
            DropTable("dbo.AccessInfoHashes");
            AddPrimaryKey("dbo.AccessInfoes", "Id");
        }
    }
}
