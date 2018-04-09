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
                new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId", "GetAddressByDogId", "GetServiceInfo" }, ISName = "Doggies", Url = "http://localhost:59317/", Root = "api/read" },
                new AdapterServer() { ContractNames = new List<string>  { "GetMathemathicFunction" }, ISName = "MathLovers", Url = "http://localhost:59317/", Root = "/api/read" },
                new AdapterServer() { ContractNames = new List<string>  { "GetAddressByOwnerId" }, ISName = "CitizenDatabank", Url = "http://localhost:59317/", Root = "/api/read" },
            };

            return asList;
        }
    }
}
