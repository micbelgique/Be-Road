namespace Contracts.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Contracts.Dal.ContractContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Contracts.Dal.ContractContext";
        }

        protected override void Seed(Contracts.Dal.ContractContext context)
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
