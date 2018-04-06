using Contracts.Models;
using System.Collections.Generic;

namespace Proxy.Dal.Mock
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
                new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" },
                new AdapterServer() { ContractNames = new List<string>  { "GetMathemathicFunction" }, ISName = "MathLovers", Url = "www.mathlovers.com/api/" },
                new AdapterServer() { ContractNames = new List<string>  { "GetAddressByOwnerId" }, ISName = "CitizenDatabank", Url = "www.citizens.com/api/" },
            };

            return asList;
        }
    }
}
