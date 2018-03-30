using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Dal.Mock;

namespace Contracts.Dal
{
    /// <summary>
    /// Class used to find an adapter server
    /// </summary>
    public class AdapterServerService
    {
        /// <summary>
        /// List of the adapter servers
        /// </summary>
        public List<AdapterServer> ADSList { get; set; }

        /// <summary>
        /// Find an adapter server with the name of the contract used
        /// </summary>
        /// <param name="name">The name of the contract used</param>
        /// <returns>The found adapter server</returns>
        private AdapterServer FindAS(string name)
        {
            return ADSList.FirstOrDefault(s => s.ContractNames.Any(cn => cn.Equals(name)));
        }

        /// <summary>
        /// Calls the api of the Information System
        /// </summary>
        public BeContractReturn Call(BeContractCall call)
        {
            var ads = FindAS(call.Id);
            if (ads != null)
            {
                Console.WriteLine($"Calling {0} at {1}", ads.ISName, ads.Url);
                return FindMock(ads, call);
            }
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };
        }

        /// <summary>
        /// Finds the Mock (Temporary)
        /// </summary>
        /// <returns>The wanted mock class</returns>
        public BeContractReturn FindMock(AdapterServer ads, BeContractCall call)
        {
            BeContractReturn res = null;
            switch (ads.ISName)
            {
                case "CitizenDatabank":
                    res = AddressMock.GetAddressByOwnerId(call);
                    break;
                case "MathLovers":
                    res = MathematicsMock.GetSumFunction(call);
                    break;
                case "Doggies":
                    res = VeterinaryMock.GetOwnerId(call);
                    break;
            }

            return res;
        }
    }
}
