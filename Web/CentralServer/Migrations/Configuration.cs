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
        }

        private void ADSSeed(ContractContext context)
        {
            List<AdapterServer> asList = new List<AdapterServer>
            {
                new AdapterServer()
                {
                    Id = 1, ContractNames = new List<BeContract>
                    {
                        context.Contracts.FirstOrDefault(c => c.Id.Equals("GetOwnerIdByDogId")),
                        context.Contracts.FirstOrDefault(c => c.Id.Equals("GetAddressByDogId")),
                        context.Contracts.FirstOrDefault(c => c.Id.Equals("GetServiceInfo"))
                    },
                    ISName = "Doggie style",
                    Url = "http://adsmock/",
                    Root = "api/read"
                },
                new AdapterServer()
                {
                    Id = 2, ContractNames = new List<BeContract>
                    {
                        context.Contracts.FirstOrDefault(c => c.Id.Equals("GetMathemathicFunction"))
                    },
                    ISName = "MathLovers",
                    Url = "http://adsmock/",
                    Root = "api/read"
                },
                new AdapterServer() {
                    Id = 3,
                    ContractNames = new List<BeContract>
                    {
                        context.Contracts.FirstOrDefault(c => c.Id.Equals("GetAddressByOwnerId"))
                    },
                    ISName = "CitizenDatabank",
                    Url = "http://adsmock/",
                    Root = "api/read"
                },
            };

            context.AdapterServers.AddOrUpdate(asList[0]);
            context.AdapterServers.AddOrUpdate(asList[1]);
            context.AdapterServers.AddOrUpdate(asList[2]);
        }
    }
}
