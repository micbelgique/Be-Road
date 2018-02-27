using Microsoft.AspNet.Identity.EntityFramework;
using PublicService.Models;
using System;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace PublicService.Dal
{
    public class PSDbInitializer : DropCreateDatabaseIfModelChanges<PSContext>
    {
        private Random rand = new Random();
        private string[] names = { "Arlen", "Artie", "Gray", "Guard", "Ladden", "Mace", "Mark", "Seno", "Jana", "Kim", "Lydia" };
        private string[] reasons = { "Speeding", "Alcohol test", "Crazy driving", "Fun", "Abuse of power" };

        protected override void Seed(PSContext context)
        {
            InitializeCars(context);
            InitializeTrainee(context);
            InitializeIdentityForEF(context);
        }
        public static void InitializeIdentityForEF(PSContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }


        private void InitializeTrainee(PSContext context)
        {
            context.MicTrainees.Add(GenerateNewTrainee("VM", "Michaël", "Gumb", "Kazakhstan", "25"));
            context.MicTrainees.Add(GenerateNewTrainee("W", "Wilson", "Mossop", "South Korea", "22"));
            context.MicTrainees.Add(GenerateNewTrainee("P", "Pierre", "Glaze", "Ghana", "18"));
            context.MicTrainees.Add(GenerateNewTrainee("Gostagayevskaya", "Kyla", "Turfin", "Russia", "30"));
            context.MicTrainees.Add(GenerateNewTrainee("Tabon", "Eula", "Jenney", "Philippines", "31"));
            context.MicTrainees.Add(GenerateNewTrainee("Bamusso", "Lyn", "Navarro", "Cameroon", "46"));
            context.MicTrainees.Add(GenerateNewTrainee("Roi Et", "Byrann", "Antal", "Thailand", "13"));
            context.MicTrainees.Add(GenerateNewTrainee("Ortigueira", "Rossie", "Marcussen", "Brazil", "16"));
            context.MicTrainees.Add(GenerateNewTrainee("Sakākā", "Lila", "Cartlidge", "Saudi Arabia", "56"));
            context.MicTrainees.Add(GenerateNewTrainee("Ikey", "Frances", "Maneylaws", "Russia", "35"));
            context.MicTrainees.Add(GenerateNewTrainee("Trojanów", "Carmen", "Shay", "Poland", "57"));
            context.MicTrainees.Add(GenerateNewTrainee("Muyinga", "Virgie", "Monkton", "Burundi", "35"));
            context.MicTrainees.Add(GenerateNewTrainee("Longquan", "Amandi", "Rieger", "China", "68"));
            context.MicTrainees.Add(GenerateNewTrainee("Belyye Stolby", "Chan", "Roston", "Russia", "5"));
            context.MicTrainees.Add(GenerateNewTrainee("Brasília", "Glen", "Reignould", "Brazil", "54"));
            context.MicTrainees.Add(GenerateNewTrainee("Fagersta", "Carrissa", "Dibnah", "Sweden", "43"));
        }

        private MicTrainee GenerateNewTrainee(string ln, string fn, string loc,  string nat, string age)
        {
            return new MicTrainee()
            {
                Age = new Data() { Name = age },
                FirstName = new Data() { Name = fn },
                LastName = new Data() { Name = ln },
                Locality = new Data() { Name = loc },
                Nationality = new Data() { Name = nat }
            };
        }


        private void InitializeCars(PSContext context)
        {
            context.Cars.Add(GenerateNewCar("Michaël", "Fiat", "1-KGG-695"));
            context.Cars.Add(GenerateNewCar("Wilson", "Nissan", "1-EBD-684"));
            context.Cars.Add(GenerateNewCar("Pierre", "Porche", "1-OZA-014"));
            context.Cars.Add(GenerateNewCar("Raph", "Citroën", "1-VNK-646"));
            context.Cars.Add(GenerateNewCar("Fred", "Mercedes", "1-ZAR-755"));
            context.Cars.Add(GenerateNewCar("Thomas", "BMW", "1-PER-124"));
            context.Cars.Add(GenerateNewCar("Martine", "Audi", "1-DFV-862"));
        }

        private Car GenerateNewCar(string owner, string brand, string numberPlate)
        {
            return new Car()
            {
                Owner = new Data()
                {
                    Name = owner,
                    AccessInfos = GenerateRandomAccessInfo(20)
                },
                Brand = new Data() {
                    Name = brand,
                    AccessInfos = GenerateRandomAccessInfo(50)
                },
                NumberPlate = new Data()
                {
                    Name = numberPlate,
                    AccessInfos = GenerateRandomAccessInfo(80)
                }
            };
        }

        private List<AccessInfo> GenerateRandomAccessInfo(int probability)
        {
            var infos = new List<AccessInfo>();
            for(int i = 0; i < 3; i++)
            {
                if (rand.Next(100) < probability)
                {
                    infos.Add(new AccessInfo() {
                        Name = names[rand.Next(names.Length)],
                        Date = new DateTime(rand.Next(2000, 2018), rand.Next(1, 12), rand.Next(1, 28)),
                        Reason =  reasons[rand.Next(reasons.Length)]
                    });
                }
            }
            return infos;
        }
    }
}