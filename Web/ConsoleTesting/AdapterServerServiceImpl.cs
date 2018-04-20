using ConsoleTesting;
using ConsoleTesting.Mock;
using Contracts;
using Contracts.Dal;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting
{
    /// <summary>
    /// Class used to find an adapter server
    /// </summary>
    public class AdapterServerServiceImpl : IAdapterServerService
    {
        public List<AdapterServer> ADSList { get; set; } = ASSMock.Fill();

        /// <summary>
        /// Find an adapter server with the name of the contract used
        /// </summary>
        /// <param name="name">The name of the contract used</param>
        /// <returns>The found adapter server</returns>
        public AdapterServer FindAS(string name)
        {
            return ADSList.FirstOrDefault(s => s.ContractNames.Any(cn => cn.Name.Equals(name)));
        }

        /// <summary>
        /// Calls the api of the Information System
        /// </summary>
        public Task<BeContractReturn> CallAsync(BeContractCall call)
        {
            var ads = FindAS(call.Id);
            if (ads != null)
            {
                Console.WriteLine($"Calling {ads.ISName} at {ads.Url}");
                return FindAsync(ads, call);
            }
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };
        }

        //Warning is nothing
        public async Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call)
        {
            switch (ads.ISName)
            {
                case "Doggies": return VeterinaryMock.GetOwnerId(call);
                case "MathLovers": return MathematicsMock.GetSumFunction(call);
                case "CitizenDatabank": return AddressMock.GetAddressByOwnerId(call);
                default: return new BeContractReturn();
            }
        }

    }
}
