namespace MessageLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Hash = c.String(),
                        TransactionAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccessInfoes");
        }
    }
}
