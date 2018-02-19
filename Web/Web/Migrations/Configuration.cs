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
            context.PublicServices.Add(new PublicService
            {
                Name = "Zippopotam",
                Description = "Check the zip code",
                Url = "http://api.zippopotam.us",
                DalMethod = "Zippo",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "FreeGeoip",
                Description = "Get the info about your IP address",
                Url = "http://freegeoip.net/json",
                DalMethod = "Geoip",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "Prenom masculins",
                Description = "Check how many times your name is used",
                Url = "https://bruxellesdata.opendatasoft.com/api/records/1.0/search/",
                DalMethod = "Firstnames",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "Itunes",
                Description = "Which star has your name ?",
                Url = "https://itunes.apple.com/search",
                DalMethod = "Itunes",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "Azure files",
                Description = "It's hosted on the cloud",
                Url = "UserBrussels.txt",
                DalMethod = "Azure",
            });
        }
    }
}
