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
            InitializeUsers(context);
            InitializeCars(context);
            InitializeIdentityForEF(context);
        }

        private void InitializeIdentityForEF(PSContext db)
        {
            InitializeIdentityAdmin(db);
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

        private void InitializeUsers(PSContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.Create(GenerateUser("1", "93011150162", "https://yt3.ggpht.com/a-/AJLlDp3l-ppFv3xLR_dg0jSFoWSbF-94mjQxob8XvQ=s900-mo-c-c0xffffffff-rj-k-no", userManager), "Admin@123456");
            userManager.Create(GenerateUser("2", "97081817718", "https://cdn.shopify.com/s/files/1/0597/9769/products/wilson-castaway_film_1024x1024.jpg?v=1421681966", userManager), "Admin@123456");
            userManager.Create(GenerateUser("3", "95052316256", "https://i.pinimg.com/originals/7e/54/cd/7e54cd815991d954c82ff191c88f157c.jpg", userManager), "Admin@123456");
            userManager.Create(GenerateUser("4", "97010215411", "https://www.lieux-insolites.fr/hsavoie/martin/pierre%20martin-2.jpg", userManager), "Admin@123456");
            userManager.Create(GenerateUser("5", "96122400226", "https://images.joueclub.fr/produits/S/06041106_2.jpg", userManager), "Admin@123456");
            userManager.Create(GenerateUser("6", "93020623433", "https://www.rollingstone.fr/RS-WP-magazine/wp-content/uploads/2016/06/mohammed-ali-400x330.jpeg", userManager), "Admin@123456");
            userManager.Create(GenerateUser("7", "95071956583", "https://consequenceofsound.files.wordpress.com/2018/03/nicolas-cage-superman-teen-titans-animated.png?w=807", userManager), "Admin@123456");
        }

        private ApplicationUser GenerateUser(string id, string userName, string url, UserManager<ApplicationUser> userManager)
        {
            var user = userManager.FindByName(userName);
            if (user != null)
            {
                userManager.Delete(user);
                user = null;
            }

            return new ApplicationUser()
            {
                Id = id,
                UserName = userName,
                PhotoUrl = url
            };
        }

        private void InitializeCars(PSContext context)
        {
            context.Cars.RemoveRange(context.Cars);
            context.Cars.Add(GenerateNewCar(1, "93011150162", context));
            context.Cars.Add(GenerateNewCar(2, "97081817718", context));
            context.Cars.Add(GenerateNewCar(3, "95052316256", context));
            context.Cars.Add(GenerateNewCar(4, "97010215411", context));
            context.Cars.Add(GenerateNewCar(5, "96122400226", context));
            context.Cars.Add(GenerateNewCar(6, "93020623433", context));
            context.Cars.Add(GenerateNewCar(7, "95071956583", context));
        }

        private Car GenerateNewCar(int id, string owner, PSContext db)
        {
            var car = new Car()
            {  
                Id = id
            };
            car.Owner = db.Users.First(u => u.UserName.Equals(owner));
            return car;
        }
    }
}
