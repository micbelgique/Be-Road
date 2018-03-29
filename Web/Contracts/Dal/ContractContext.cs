using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal
{
    public class ContractContext : DbContext
    {
        public ContractContext() : base("name=ContractContext")
        {
            
        }

        public DbSet<BeContract> Contracts { get; set; }
    }
}
