namespace PublicService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullReworkBeContract : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccessInfoes", "Data_Id", "dbo.Data");
            DropForeignKey("dbo.Cars", "Brand_Id", "dbo.Data");
            DropForeignKey("dbo.Cars", "NumberPlate_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "BirthDate_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "EmailAddress_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "ExtraInfo_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "FirstName_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "LastName_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "Locality_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "Nationality_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "PhotoUrl_Id", "dbo.Data");
            DropIndex("dbo.Cars", new[] { "Brand_Id" });
            DropIndex("dbo.Cars", new[] { "NumberPlate_Id" });
            DropIndex("dbo.Cars", new[] { "Owner_Id" });
            DropIndex("dbo.AccessInfoes", new[] { "Data_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "BirthDate_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "EmailAddress_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "ExtraInfo_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "FirstName_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "LastName_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Locality_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Nationality_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "PhotoUrl_Id" });
            AddColumn("dbo.AspNetUsers", "PhotoUrl", c => c.String());
            DropColumn("dbo.Cars", "Brand_Id");
            DropColumn("dbo.Cars", "NumberPlate_Id");
            DropColumn("dbo.AspNetUsers", "NRID");
            DropColumn("dbo.AspNetUsers", "BirthDate_Id");
            DropColumn("dbo.AspNetUsers", "EmailAddress_Id");
            DropColumn("dbo.AspNetUsers", "ExtraInfo_Id");
            DropColumn("dbo.AspNetUsers", "FirstName_Id");
            DropColumn("dbo.AspNetUsers", "LastName_Id");
            DropColumn("dbo.AspNetUsers", "Locality_Id");
            DropColumn("dbo.AspNetUsers", "Nationality_Id");
            DropColumn("dbo.AspNetUsers", "PhotoUrl_Id");
            DropTable("dbo.Cars");
            CreateTable(
                "dbo.Cars",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Owner_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Data");
            DropTable("dbo.AccessInfoes");

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AccessInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Reason = c.String(),
                        Date = c.DateTime(nullable: false),
                        Data_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Data",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "PhotoUrl_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Nationality_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Locality_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "LastName_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "FirstName_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ExtraInfo_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "EmailAddress_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "BirthDate_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "NRID", c => c.String());
            AddColumn("dbo.Cars", "NumberPlate_Id", c => c.Int());
            AddColumn("dbo.Cars", "Brand_Id", c => c.Int());
            DropIndex("dbo.Cars", new[] { "Owner_Id" });
            AlterColumn("dbo.Cars", "Owner_Id", c => c.Int());
            DropColumn("dbo.AspNetUsers", "PhotoUrl");
            CreateIndex("dbo.AspNetUsers", "PhotoUrl_Id");
            CreateIndex("dbo.AspNetUsers", "Nationality_Id");
            CreateIndex("dbo.AspNetUsers", "Locality_Id");
            CreateIndex("dbo.AspNetUsers", "LastName_Id");
            CreateIndex("dbo.AspNetUsers", "FirstName_Id");
            CreateIndex("dbo.AspNetUsers", "ExtraInfo_Id");
            CreateIndex("dbo.AspNetUsers", "EmailAddress_Id");
            CreateIndex("dbo.AspNetUsers", "BirthDate_Id");
            CreateIndex("dbo.AccessInfoes", "Data_Id");
            CreateIndex("dbo.Cars", "Owner_Id");
            CreateIndex("dbo.Cars", "NumberPlate_Id");
            CreateIndex("dbo.Cars", "Brand_Id");
            AddForeignKey("dbo.AspNetUsers", "PhotoUrl_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AspNetUsers", "Nationality_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AspNetUsers", "Locality_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AspNetUsers", "LastName_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AspNetUsers", "FirstName_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AspNetUsers", "ExtraInfo_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AspNetUsers", "EmailAddress_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AspNetUsers", "BirthDate_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.Cars", "NumberPlate_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.Cars", "Brand_Id", "dbo.Data", "Id");
            AddForeignKey("dbo.AccessInfoes", "Data_Id", "dbo.Data", "Id");
        }
    }
}
