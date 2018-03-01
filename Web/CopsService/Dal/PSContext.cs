using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PublicService.Dal
{
    public class PSContext : IdentityDbContext<ApplicationUser>
    {
        public PSContext() : base("PSContext")
        {

        }

        static PSContext()
        {
            Database.SetInitializer(new PSDbInitializer());
        }

        public static PSContext Create()
        {
            return new PSContext();
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Data> Datas { get; set; }
    }

}