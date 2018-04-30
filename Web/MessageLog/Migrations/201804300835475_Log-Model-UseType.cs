namespace MessageLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogModelUseType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "UseType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "UseType");
        }
    }
}
