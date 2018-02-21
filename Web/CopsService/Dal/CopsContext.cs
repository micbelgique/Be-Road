using CopsService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CopsService.Dal
{
    public class CopsContext : DbContext
    {
        public CopsContext() : base("CopsContext")
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}