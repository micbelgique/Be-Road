using CentralServer.Helpers;
using CentralServer.Migrations;
using Contracts.Models;
using System.Data.Entity;

namespace CentralServer.Dal
{
    public class ContractContext : DbContext
    {
        public ContractContext() : base(ConfigHelper.GetAppSetting("ContractContext"))
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContractContext, Configuration>());

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
        public DbSet<ContractName> ContractNames { get; set; }
    }
}
