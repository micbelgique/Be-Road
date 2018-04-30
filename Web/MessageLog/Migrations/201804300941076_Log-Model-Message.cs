namespace MessageLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogModelMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Message");
        }
    }
}
