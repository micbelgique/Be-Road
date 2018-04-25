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

            return asList;
        }
    }
}
