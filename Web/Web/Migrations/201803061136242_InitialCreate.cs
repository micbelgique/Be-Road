namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublicServices", "ImageURI", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PublicServices", "ImageURI");
        }
    }
}
