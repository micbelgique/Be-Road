namespace MessageLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogModelAccessInfoRem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "Access_Id", "dbo.AccessInfoes");
            DropIndex("dbo.Logs", new[] { "Access_Id" });
            DropColumn("dbo.Logs", "Access_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "Access_Id", c => c.Int());
            CreateIndex("dbo.Logs", "Access_Id");
            AddForeignKey("dbo.Logs", "Access_Id", "dbo.AccessInfoes", "Id");
        }
    }
}
