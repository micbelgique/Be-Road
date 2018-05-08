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
                new AdapterServer()
                {
                    Id = 1, ContractNames = new List<BeContract>
                    {
                        new BeContract() { Id = "GetOwnerIdByDogId" },
                        new BeContract() { Id = "GetAddressByDogId" },
                        new BeContract() { Id = "GetServiceInfo" }
                    },
                    ISName = "Doggies",
                    Url = "http://adsmock/",
                    Root = "api/read"
                },
                new AdapterServer()
                {
                    Id = 2, ContractNames = new List<BeContract>
                    {
                        new BeContract() { Id = "GetMathemathicFunction" }
                    },
                    ISName = "MathLovers",
                    Url = "http://adsmock/",
                    Root = "api/read"
                },
                new AdapterServer() {
                    Id = 3,
                    ContractNames = new List<BeContract>
                    {
                        new BeContract() { Id = "GetAddressByOwnerId" }
                    },
                    ISName = "CitizenDatabank",
                    Url = "http://adsmock/",
                    Root = "api/read"
                },
            };

            return asList;
        }
    }
}
