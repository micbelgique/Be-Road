namespace CentralServer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CentralServer.Dal.ContractContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CentralServer.Dal.ContractContext context)
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
    }
}
