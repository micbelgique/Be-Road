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

namespace BeRoadTest.ADSMock
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
        }

        /// <summary>
        /// Find an adapter server with the name of the contract used
        /// </summary>
        /// <param name="name">The name of the contract used</param>
        /// <returns>The found adapter server</returns>
        public async Task<AdapterServer> FindASAsync(string name)
        {
            AdapterServer ads = null;
            await Task.Run(() => ads = ADSList.FirstOrDefault(s => s.ContractNames.Any(cn => cn.Id.Equals(name))));
            return ads;
        }
        

        public async Task<BeContractReturn> CallAsync(AdapterServer ads, BeContractCall call)
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
