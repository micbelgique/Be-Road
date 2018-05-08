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
        public PSContext() : base()
        {

        }

        public PSContext(string connectionString) : base(connectionString)
        {

        }

        public DbSet<PublicService> PublicServices { get; set; }
    }
}