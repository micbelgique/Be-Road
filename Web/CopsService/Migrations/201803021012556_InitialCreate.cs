namespace PublicService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand_Id = c.Int(),
                        NumberPlate_Id = c.Int(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Data", t => t.Brand_Id)
                .ForeignKey("dbo.Data", t => t.NumberPlate_Id)
                .ForeignKey("dbo.Data", t => t.Owner_Id)
                .Index(t => t.Brand_Id)
                .Index(t => t.NumberPlate_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Data",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Data", t => t.Data_Id)
                .Index(t => t.Data_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        BirthDate_Id = c.Int(),
                        EmailAddress_Id = c.Int(),
                        ExtraInfo_Id = c.Int(),
                        FirstName_Id = c.Int(),
                        LastName_Id = c.Int(),
                        Locality_Id = c.Int(),
                        Nationality_Id = c.Int(),
                        PhotoUrl_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Data", t => t.BirthDate_Id)
                .ForeignKey("dbo.Data", t => t.EmailAddress_Id)
                .ForeignKey("dbo.Data", t => t.ExtraInfo_Id)
                .ForeignKey("dbo.Data", t => t.FirstName_Id)
                .ForeignKey("dbo.Data", t => t.LastName_Id)
                .ForeignKey("dbo.Data", t => t.Locality_Id)
                .ForeignKey("dbo.Data", t => t.Nationality_Id)
                .ForeignKey("dbo.Data", t => t.PhotoUrl_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.BirthDate_Id)
                .Index(t => t.EmailAddress_Id)
                .Index(t => t.ExtraInfo_Id)
                .Index(t => t.FirstName_Id)
                .Index(t => t.LastName_Id)
                .Index(t => t.Locality_Id)
                .Index(t => t.Nationality_Id)
                .Index(t => t.PhotoUrl_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PhotoUrl_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "Nationality_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Locality_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "LastName_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "FirstName_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "ExtraInfo_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUsers", "EmailAddress_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BirthDate_Id", "dbo.Data");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Cars", "Owner_Id", "dbo.Data");
            DropForeignKey("dbo.Cars", "NumberPlate_Id", "dbo.Data");
            DropForeignKey("dbo.Cars", "Brand_Id", "dbo.Data");
            DropForeignKey("dbo.AccessInfoes", "Data_Id", "dbo.Data");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "PhotoUrl_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Nationality_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Locality_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "LastName_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "FirstName_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "ExtraInfo_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "EmailAddress_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "BirthDate_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AccessInfoes", new[] { "Data_Id" });
            DropIndex("dbo.Cars", new[] { "Owner_Id" });
            DropIndex("dbo.Cars", new[] { "NumberPlate_Id" });
            DropIndex("dbo.Cars", new[] { "Brand_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AccessInfoes");
            DropTable("dbo.Data");
            DropTable("dbo.Cars");
        }
    }
}
