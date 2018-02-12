using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Dal
{
    public class PSDatabaseInitializer : DropCreateDatabaseAlways<PSContext>
    {
        protected override void Seed(PSContext context)
        {
            context.PublicServices.Add(new PublicService
            {
                Name = "Zippopotam",
                Description = "Check the zip code",
                Url = "http://api.zippopotam.us",
                DalMethod = "zippo",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "FreeGeoip",
                Description = "Get the info about your IP address",
                Url = "http://freegeoip.net",
                DalMethod = "geoip",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "Conseil communal",
                Description = "Info about the \"VIP's\"",
                Url = "https://opendata.brussels.be/api/records/1.0/search/?dataset=conseil-communal",
                DalMethod = "communal",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "Prenom masculins",
                Description = "Check how many times your name is used",
                Url = "https://bruxellesdata.opendatasoft.com/api/records/1.0/search/?dataset=prenoms-masculins-20150",
                DalMethod = "firstnames",
            });
            context.PublicServices.Add(new PublicService
            {
                Name = "Itunes",
                Description = "Which star has your name ?",
                Url = "https://itunes.apple.com/search",
                DalMethod = "itunes",
            });
        }
    }
}