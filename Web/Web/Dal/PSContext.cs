using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Dal
{
    public class PSContext : DbContext
    {
        public PSContext() : base("PSContext")
        {

        }

        public DbSet<PublicService> PublicServices { get; set; }
    }
}