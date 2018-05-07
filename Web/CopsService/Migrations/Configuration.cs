namespace PublicService.Migrations
{
    using Contracts.Models;
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
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;

    internal sealed class Configuration : DbMigrationsConfiguration<PSContext>
    {

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
            //Delete the current admin because we want to update him
            if (user != null)
            {
                userManager.Delete(user);
                user = null;
            }
            if (user == null)
            {
                user = new ApplicationUser {
                    UserName = name,
                };
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
            context.Cars.AddOrUpdate(GenerateNewCar(1, "93011150162"));
            context.Cars.AddOrUpdate(GenerateNewCar(2, "97081817718"));
            context.Cars.AddOrUpdate(GenerateNewCar(3, "95052316256"));
            context.Cars.AddOrUpdate(GenerateNewCar(4, "97010215411"));
            context.Cars.AddOrUpdate(GenerateNewCar(5, "96122400226"));
            context.Cars.AddOrUpdate(GenerateNewCar(6, "93020623433"));
            context.Cars.AddOrUpdate(GenerateNewCar(7, "95071956583"));
        }

        private Car GenerateNewCar(int id, string owner)
        {
            PSContext db = new PSContext();
            return new Car()
            {  
                Id = id,
                Owner = db.Users.FirstOrDefault(u => u.UserName.Equals(owner))
            };
        }
    }
}
