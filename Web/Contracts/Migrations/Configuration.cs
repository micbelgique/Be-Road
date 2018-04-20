namespace Contracts.Migrations
{
    using Contracts.Dal;
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
            AutomaticMigrationsEnabled = false;
            ContextKey = "Contracts.Dal.ContractContext";
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
            List<AdapterServer> asList = new List<AdapterServer>()
            {
                new AdapterServer() { Id = 1, ContractNames = new List<ContractName>(), ISName = "Doggie style", Url = "http://localhost:59317/", Root = "api/read" },
                new AdapterServer() { Id = 2, ContractNames = new List<ContractName>(), ISName = "MathLovers", Url = "http://localhost:59317/", Root = "/api/read" },
                new AdapterServer() { Id = 3, ContractNames = new List<ContractName>(), ISName = "CitizenDatabank", Url = "http://localhost:59317/", Root = "/api/read" },
            };

            asList[0].ContractNames.Add(new ContractName() { Id = 1, Name = "GetOwnerIdByDogId" });
            asList[0].ContractNames.Add(new ContractName() { Id = 2, Name = "GetAddressByDogId" });
            asList[0].ContractNames.Add(new ContractName() { Id = 3, Name = "GetServiceInfo" });
            asList[1].ContractNames.Add(new ContractName() { Id = 4, Name = "GetMathemathicFunction" });
            asList[2].ContractNames.Add(new ContractName() { Id = 5, Name = "GetAddressByOwnerId" });

            context.AdapterServers.AddOrUpdate(asList[0]);
            context.AdapterServers.AddOrUpdate(asList[1]);
            context.AdapterServers.AddOrUpdate(asList[2]);
        }
    }
}
