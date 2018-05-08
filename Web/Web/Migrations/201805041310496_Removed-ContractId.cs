namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedContractId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PublicServices", "ContractId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PublicServices", "ContractId", c => c.String(nullable: false));
        }
    }
}
