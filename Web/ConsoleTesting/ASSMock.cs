using Contracts.Models;
using System.Collections.Generic;

namespace ConsoleTesting
{
    /// <summary>
    /// Class used to fill the Adapter Server list for the tests
    /// </summary>
    public class ASSMock
    {
        /// <summary>
        /// Fills the the Adapter Server list
        /// </summary>
        /// <returns>List of Adapter Servers</returns>
        public static List<AdapterServer> Fill()
        {
            List<AdapterServer> asList = new List<AdapterServer>
            {
                new AdapterServer() { Id = 1, ContractNames = new List<ContractName> { new ContractName() { Name = "GetOwnerIdByDogId" }, new ContractName() { Name = "GetAddressByDogId" }, new ContractName() { Name = "GetServiceInfo" } }, ISName = "Doggies", Url = "http://localhost:59317/", Root = "api/read" },
                new AdapterServer() { Id = 2, ContractNames = new List<ContractName>  { new ContractName() { Name = "GetMathemathicFunction" } }, ISName = "MathLovers", Url = "http://localhost:59317/", Root = "/api/read" },
                new AdapterServer() { Id = 3, ContractNames = new List<ContractName>  { new ContractName() { Name = "GetAddressByOwnerId" } }, ISName = "CitizenDatabank", Url = "http://localhost:59317/", Root = "/api/read" },
            };

            return asList;
        }
    }
}
