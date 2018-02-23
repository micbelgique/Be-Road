using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PublicService.Dal
{
    public class PSContext : DbContext
    {
        public PSContext() : base("PSContext")
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}