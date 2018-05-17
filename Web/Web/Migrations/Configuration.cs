namespace Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Web.Dal;
    using Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PSContext>
    {
        public Configuration()
        {
        }

        protected override void Seed(PSContext context)
        {
            context.PublicServices.AddOrUpdate(ps => ps.Name,
                   new PublicService
                   {
                       Name = "Population Service",
                       Description = "Belgium administration for the population",
                       ImageURI = "/Content/img/ps.jpg"
                   },
                   new PublicService
                   {
                       ID = 2,
                       Name = "Div Service",
                       Description = "Direction Immatriculation des Véhicules",
                       ImageURI = "/Content/img/immatriculation.jpg"
                   },
                   new PublicService
                   {
                       ID = 3,
                       Name = "Bank Service",
                       Description = "Contains information about your bankaccount",
                       ImageURI = "/Content/img/bank.png"
                   },
                   new PublicService
                   {
                       ID = 4,
                       Name = "Funny Service",
                       Description = "This public service contains funny data about you",
                       ImageURI = "/Content/img/funny.png"
                   }
           );
        }
        
    }
}
