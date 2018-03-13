namespace PublicService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountsWithoutDevs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NRID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NRID");
        }
    }
}
