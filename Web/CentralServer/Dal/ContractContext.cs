﻿using CentralServer.Helpers;
using Contracts.Models;
using System.Data.Entity;

namespace CentralServer.Dal
{
    public class ContractContext : DbContext
    {
        public ContractContext()
        {

        }

        public ContractContext(string context) : base(context)
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

            modelBuilder.Entity<Query>()
                .HasMany(q => q.Mappings)
                .WithOptional()
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BeContract> Contracts { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Mapping> Mappings { get; set; }
        public DbSet<Output> Outputs { get; set; }
        public DbSet<AdapterServer> AdapterServers { get; set; }
    }
}
