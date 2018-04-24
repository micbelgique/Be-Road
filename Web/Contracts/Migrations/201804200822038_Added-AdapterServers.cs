namespace Contracts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAdapterServers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdapterServers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISName = c.String(),
                        Url = c.String(),
                        Root = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdapterServers");
        }
    }
}
