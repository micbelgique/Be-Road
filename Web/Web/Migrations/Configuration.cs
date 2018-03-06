namespace Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Dal.PSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.Dal.PSContext context)
        {
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 1,
                Name = "Public Service",
                Description = "Who's reading your profile on the public service ?",
                Url = "users.json",
                DalMethod = "PublicService",
                ImageURI = "/Content/img/ps.jpg"
            });
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 2,
                Name = "MIC Data",
                Description = "Who's reading your profile at the MIC ?",
                Url = "mic.json",
                DalMethod = "Mic",
                ImageURI = "/Content/img/mic.jpg"
            });
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 3,
                Name = "Cops Data",
                Description = "Check who's cops has read your data",
                Url = "cops.json",
                DalMethod = "Cops",
                ImageURI = "/Content/img/police.jpg"
            });
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 4,
                Name = "Zippopotam",
                Description = "Check the zip code",
                Url = "http://api.zippopotam.us",
                DalMethod = "Zippo",
                ImageURI = "/Content/img/zippopotam.png"
            });
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 5,
                Name = "FreeGeoip",
                Description = "Get the info about your IP address",
                Url = "http://freegeoip.net/json",
                DalMethod = "Geoip",
                ImageURI = "/Content/img/freegoip.png"
            });
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 6,
                Name = "Prenom masculins",
                Description = "Check how many times your name is used",
                Url = "https://bruxellesdata.opendatasoft.com/api/records/1.0/search/",
                DalMethod = "Firstnames",
                ImageURI = "/Content/img/firstnames.png"
            });
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 7,
                Name = "Itunes",
                Description = "Which star has your name ?",
                Url = "https://itunes.apple.com/search",
                DalMethod = "Itunes",
                ImageURI = "/Content/img/itunes.png"
            });
            context.PublicServices.AddOrUpdate(new PublicService
            {
                ID = 8,
                Name = "Azure files",
                Description = "It's hosted on the cloud",
                Url = "UserBrussels.txt",
                DalMethod = "Azure",
                ImageURI = "/Content/img/azure.png"
            });
        }
    }
}
