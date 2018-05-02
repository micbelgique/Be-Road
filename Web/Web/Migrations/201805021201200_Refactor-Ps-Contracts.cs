namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorPsContracts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublicServices", "ContractId", c => c.String(nullable: false));
            DropColumn("dbo.PublicServices", "DalMethod");
            DropColumn("dbo.PublicServices", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PublicServices", "Url", c => c.String(nullable: false));
            AddColumn("dbo.PublicServices", "DalMethod", c => c.String(nullable: false));
            DropColumn("dbo.PublicServices", "ContractId");
        }
    }
}
