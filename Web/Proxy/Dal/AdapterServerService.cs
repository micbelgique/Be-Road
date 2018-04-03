using Proxy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proxy.Dal.Mock;

namespace Proxy.Dal
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
                Console.WriteLine($"Calling {ads.ISName} at {ads.Url}");
                return CentralServer.FindMock(ads, call);
            }
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };
        }

        
    }
}
