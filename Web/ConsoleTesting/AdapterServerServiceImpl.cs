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
        public async Task<AdapterServer> FindASAsync(string name)
        {
            AdapterServer ads = null;
            await Task.Run(() => ads = ADSList.FirstOrDefault(s => s.ContractNames.Any(cn => cn.Name.Equals(name))));
            return ads;
        }

        /// <summary>
        /// Calls the api of the Information System
        /// </summary>
        public async Task<BeContractReturn> CallAsync(BeContractCall call)
        {
            var ads = await FindASAsync(call.Id);
            if (ads != null)
            {
                Console.WriteLine($"Calling {ads.ISName} at {ads.Url}");
                return await FindAsync(ads, call);
            }
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };
        }

        //Warning is nothing
        public async Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call)
        {
            BeContractReturn ret = null;
            await Task.Run(() =>
            {
                switch (ads.ISName)
                {
                    case "Doggies": ret = VeterinaryMock.GetOwnerId(call); break;
                    case "MathLovers": ret = MathematicsMock.GetSumFunction(call); break;
                    case "CitizenDatabank": ret = AddressMock.GetAddressByOwnerId(call); break;
                    default: ret = new BeContractReturn(); break;
                }
            });
            return ret;
        }

    }
}
