namespace Proxy.Migrations
{
    using Proxy.Models;
    using Proxy.Dal.Mock;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Proxy.Dal.ContractContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Dal.ContractContext context)
        {
            List<BeContract> contracts = BeContractsMock.GetContracts(context);
            contracts.ForEach(c => context.Contracts.AddOrUpdate(c));
            context.SaveChanges();
        }
    }
}
