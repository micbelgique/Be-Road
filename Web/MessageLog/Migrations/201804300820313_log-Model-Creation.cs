namespace MessageLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logModelCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deter = c.Int(nullable: false),
                        ContractId = c.String(),
                        Response = c.Boolean(nullable: false),
                        UserType = c.String(),
                        UserName = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Access_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccessInfoes", t => t.Access_Id)
                .Index(t => t.Access_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "Access_Id", "dbo.AccessInfoes");
            DropIndex("dbo.Logs", new[] { "Access_Id" });
            DropTable("dbo.Logs");
        }
    }
}
