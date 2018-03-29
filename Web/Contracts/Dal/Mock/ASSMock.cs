using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    /// <summary>
    /// Class used to fill the Adapter Server list for the tests
    /// </summary>
    class ASSMock
    {
        /// <summary>
        /// Fills the the Adapter Server list
        /// </summary>
        /// <returns>List of Adapter Servers</returns>
        public static List<AdapterServer> Fill()
        {
            List<AdapterServer> asList = new List<AdapterServer>
            {
                new AdapterServer() { ContractNames = { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" },
                new AdapterServer() { ContractNames = { "GetMathemathicFunction" }, ISName = "MathLovers", Url = "www.mathlovers.com/api/" },
                new AdapterServer() { ContractNames = { "GetAddressByOwnerId" }, ISName = "CitizenDatabank", Url = "www.doggies.com/api/" }
            };

            return asList;
        }
    }
}
