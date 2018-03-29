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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeContract>()
                .HasMany(c => c.Inputs)
                .WithOptional()
                .WillCascadeOnDelete();

            modelBuilder.Entity<BeContract>()
                .HasMany(c => c.Queries)
                .WithOptional()
                .WillCascadeOnDelete();

            modelBuilder.Entity<BeContract>()
                .HasMany(c => c.Outputs)
                .WithOptional()
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BeContract> Contracts { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Mapping> Mappings { get; set; }
        public DbSet<Output> Outputs { get; set; }
    }
}
