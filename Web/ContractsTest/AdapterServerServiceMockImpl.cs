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

namespace BeRoadTest
{
    /// <summary>
    /// Class used to find an adapter server
    /// </summary>
    public class AdapterServerServiceMockImpl : IAdapterServerService
    {
        public List<AdapterServer> ADSList{ get; set; }

        public AdapterServerServiceMockImpl()
        {
            ADSList = new List<AdapterServer>
            {
                new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId", "GetAddressByDogId", "GetServiceInfo" }, ISName = "Doggies", Url = "http://localhost:59317/", Root = "api/read" },
                new AdapterServer() { ContractNames = new List<string>  { "GetMathemathicFunction" }, ISName = "MathLovers", Url = "http://localhost:59317/", Root = "/api/read" },
                new AdapterServer() { ContractNames = new List<string>  { "GetAddressByOwnerId" }, ISName = "CitizenDatabank", Url = "http://localhost:59317/", Root = "/api/read" },
            };
        }

        /// <summary>
        /// Find an adapter server with the name of the contract used
        /// </summary>
        /// <param name="name">The name of the contract used</param>
        /// <returns>The found adapter server</returns>
        public AdapterServer FindAS(string name)
        {
            return ADSList.FirstOrDefault(s => s.ContractNames.Any(cn => cn.Equals(name)));
        }

        /// <summary>
        /// Calls the api of the Information System
        /// </summary>
        public async Task<BeContractReturn> CallAsync(BeContractCall call)
        {
            var ads = FindAS(call.Id);
            if (ads != null)
            {
                Console.WriteLine($"Calling {ads.ISName} at {ads.Url}");
                return await FindAsync(ads, call);
            }
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };
        }

        public async Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call)
        {
            //Empty await for the method
            await Task.Run(() => { });
            switch (call.Id)
            {
                case "GetOwnerIdByDogId": return HandleGetOwnerIdByDogId(call);
                default: return new BeContractReturn();
            }
        }

        private BeContractReturn HandleGetOwnerIdByDogId(BeContractCall call)
        {
            var ret = new BeContractReturn()
            {
                Id = call.Id,
                Outputs = new Dictionary<string, dynamic>()
            };
            if ((call.Inputs["DogID"] as string).Equals("D-123"))
                ret.Outputs.Add("OwnerIDOfTheDog", "Wilson !");
            return ret;
        }
    }
}
