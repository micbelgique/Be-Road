namespace CentralServer.Migrations
{
    using CentralServer.Dal;
    using Contracts.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContractContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ContractContext context)
        {
            ContractSeed(context);
            ADSSeed(context);
        }

        private void ContractSeed(ContractContext context)
        {
            context.Contracts.AddOrUpdate(BeContractsMock.GetServiceInfo());
            context.Contracts.AddOrUpdate(BeContractsMock.GetMathemathicFunction());
            var ownerIdByDogIdContract = BeContractsMock.GetOwnerIdByDogId();
            var addressByOwnerIdContract = BeContractsMock.GetAddressByOwnerId();
            var addressByDogIdContract = BeContractsMock.GetAddressByDogId();
            addressByDogIdContract.Queries[0].Contract = ownerIdByDogIdContract;
            addressByDogIdContract.Queries[1].Contract = addressByOwnerIdContract;
            context.Contracts.AddOrUpdate(ownerIdByDogIdContract);
            context.Contracts.AddOrUpdate(addressByOwnerIdContract);
            context.Contracts.AddOrUpdate(addressByDogIdContract);
            context.Contracts.AddOrUpdate(BeContractsMock.GetDoubleInputContract());
            context.SaveChanges();
        }

        private void ADSSeed(ContractContext context)
        {
            var cn1 = new List<BeContract>
            {
                context.Contracts.FirstOrDefault(c => c.Id.Equals("GetOwnerIdByDogId")),
                context.Contracts.FirstOrDefault(c => c.Id.Equals("GetAddressByOwnerId")),
                context.Contracts.FirstOrDefault(c => c.Id.Equals("GetAddressByDogId")),
                context.Contracts.FirstOrDefault(c => c.Id.Equals("GetServiceInfo"))
            };

            var cn2 = new List<BeContract>
            {
                context.Contracts.FirstOrDefault(c => c.Id.Equals("GetMathemathicFunction"))
            };

            var cn3 = new List<BeContract>
            {
                context.Contracts.FirstOrDefault(c => c.Id.Equals("DoubleInputContract"))
            };

            var ads1 = new AdapterServer()
            {
                Id = 1,
                ContractNames = cn1,
                ISName = "Doggie style",
                Url = "http://adsmock/",
                Root = "api/read"
            };
            var ads2 = new AdapterServer()
            {
                Id = 2,
                ContractNames = cn2,
                ISName = "MathLovers",
                Url = "http://adsmock/",
                Root = "api/read"
            };
            var ads3 = new AdapterServer()
            {
                Id = 3,
                ContractNames = cn3,
                ISName = "CitizenDatabank",
                Url = "http://adsmock/",
                Root = "api/read"
            };

            var ads1db = context.AdapterServers.FirstOrDefault(ads => ads.ISName == ads1.ISName);
            var ads2db = context.AdapterServers.FirstOrDefault(ads => ads.ISName == ads2.ISName);
            var ads3db = context.AdapterServers.FirstOrDefault(ads => ads.ISName == ads3.ISName);

            if (ads1db != null)
                context.AdapterServers.Remove(ads1db);

            if (ads2db != null)
                context.AdapterServers.Remove(ads2db);

            if (ads3db != null)
                context.AdapterServers.Remove(ads3db);

            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();

            context.AdapterServers.Add(ads1);
            context.AdapterServers.Add(ads2);
            context.AdapterServers.Add(ads3);
            context.SaveChanges();
        }
    }
}
