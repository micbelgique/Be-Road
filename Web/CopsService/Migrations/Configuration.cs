namespace PublicService.Migrations
{
    using CsvHelper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Newtonsoft.Json;
    using PublicService.Dal;
    using PublicService.Managers;
    using PublicService.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Hosting;

    internal sealed class Configuration : DbMigrationsConfiguration<PSContext>
    {
        private Random rand = new Random();
        private string[] names = { "Arlen", "Artie", "Gray", "Guard", "Ladden", "Mace", "Mark", "Seno", "Jana", "Kim", "Lydia" };
        private string[] reasons = { "Speeding", "Alcohol test", "Crazy driving", "Fun", "Abuse of power" };

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PublicService.Dal.PSContext";
        }

        protected override void Seed(PSContext context)
        {
            InitializeCars(context);
            InitializeIdentityForEF(context);
        }

        private void InitializeIdentityForEF(PSContext db)
        {
            InitializeIdentityAdmin(db);
            InitializeIdentityUsers(db);
            DeleteDevs(db);
        }

        private void DeleteDevs(PSContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var mic = userManager.FindByName("Michael..Van.Meerbeek@Mons");
            var wil = userManager.FindByName("Wilson.Weets@Mons");

            if (mic != null && wil != null)
            {
                userManager.Delete(mic);
                userManager.Delete(wil);
            }
        }

        private void InitializeIdentityUsers(PSContext db)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            string homeFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", @"App_Data\stagiaires.csv");
            using (StreamReader reader = File.OpenText(homeFile))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ";";
                csv.Configuration.MissingFieldFound = null;
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var fn = csv.GetField("Prenom");
                    var ln = csv.GetField("Nom");
                    var school = csv.GetField("Ecole");
                    var stage = csv.GetField("Ville de stage");
                    var mail = csv.GetField("Email MIC");
                    var loc = csv.GetField("Ville domicile");
                    var url = csv.GetField("URL photo");
                    var info = csv.GetField("Extra Info");

                    var userName = $"{fn.Replace(' ', '.')}.{ln.Replace(' ', '.')}@{stage}";
                    var password = "Admin@123456";

                    var user = userManager.FindByName(userName);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            //Set an ID, else we create duplicate data
                            //Id = userName,
                            UserName = userName,
                            FirstName = new Data { Value = fn },
                            LastName = new Data { Value = ln },
                            Locality = new Data { Value = loc },
                            //Nationality is empty
                            //School stage isn't in the model
                            PhotoUrl = new Data { Value = url },
                            ExtraInfo = new Data { Value = info },
                            EmailAddress = new Data { Value = mail }
                        };

                        //throw new Exception(user.UserName);
                        var result = userManager.Create(user, password);
                        result = userManager.SetLockoutEnabled(user.Id, false);
                    }
                }
            }
        }

        private void InitializeIdentityAdmin(PSContext db)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new ApplicationRoleManager(roleStore);

            const string name = "admin";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            //Create the admin
            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name };
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

        private void InitializeCars(PSContext context)
        {
            context.Cars.AddOrUpdate(GenerateNewCar(1, "Michaël", "Fiat", "1-KGG-695"));
            context.Cars.AddOrUpdate(GenerateNewCar(2, "Wilson", "Nissan", "1-EBD-684"));
            context.Cars.AddOrUpdate(GenerateNewCar(3, "Pierre", "Porche", "1-OZA-014"));
            context.Cars.AddOrUpdate(GenerateNewCar(4, "Raph", "Citroën", "1-VNK-646"));
            context.Cars.AddOrUpdate(GenerateNewCar(5, "Fred", "Mercedes", "1-ZAR-755"));
            context.Cars.AddOrUpdate(GenerateNewCar(6, "Thomas", "BMW", "1-PER-124"));
            context.Cars.AddOrUpdate(GenerateNewCar(7, "Martine", "Audi", "1-DFV-862"));
        }

        private Car GenerateNewCar(int id, string owner, string brand, string numberPlate)
        {
            return new Car()
            {  
                Id = id,
                Owner = new Data()
                {
                    Value = owner,
                    AccessInfos = GenerateRandomAccessInfo(20)
                },
                Brand = new Data()
                {
                    Value = brand,
                    AccessInfos = GenerateRandomAccessInfo(50)
                },
                NumberPlate = new Data()
                {
                    Value = numberPlate,
                    AccessInfos = GenerateRandomAccessInfo(80)
                }
            };
        }

        private List<AccessInfo> GenerateRandomAccessInfo(int probability)
        {
            var infos = new List<AccessInfo>();
            for (int i = 0; i < 3; i++)
            {
                if (rand.Next(100) < probability)
                {
                    infos.Add(new AccessInfo()
                    {
                        Name = names[rand.Next(names.Length)],
                        Date = new DateTime(rand.Next(2000, 2018), rand.Next(1, 12), rand.Next(1, 28)),
                        Reason = reasons[rand.Next(reasons.Length)]
                    });
                }
            }
            return infos;
        }
    }
}
