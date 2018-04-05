using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using ContractsTest;
using Newtonsoft.Json;
using Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ADSMock.Controllers
{
    [RoutePrefix("api/call")]
    public class CallController : ApiController {
        /// <summary>
        /// Calls Be-Road to find the dog owner's ID
        /// </summary>
        /// <param name="dogId">The ID of the dog</param>
        /// <returns>The dog owner's ID</returns>
        [HttpGet]
        [Route("ownerbydog/{dogid}")]
        public string GetOwnerByDog(string dogId) {
            // This section will be in the proxy later
            AdapterServerService ass = null;

            var cm = new ContractManager(ass = new AdapterServerService());
            ass.SetADSList(ASSMock.Fill());

            var ownerByDog = new BeContractCall()
            {
                Id = "GetOwnerIdByDogId",
                Inputs = new Dictionary<string, dynamic>()
                {
                    { "DogID", dogId },
                }
            };

            // Another call will be made (to the API of the Proxy)
            var res = cm.Call(ownerByDog);
            
            return res.Outputs.FirstOrDefault().Value;
        }

        /// <summary>
        /// Calls Be-Road to find the dog address
        /// </summary>
        /// <param name="dogId">The ID of the dog</param>
        /// <returns>The dog address</returns>
        [HttpGet]
        [Route("addrbydog/{dogid}")]
        public string GetAddressByDog(string dogId)
        {
            // This section will be in the proxy later
            AdapterServerService ass = null;

            var cm = new ContractManager(ass = new AdapterServerService());
            ass.SetADSList(ASSMock.Fill());

            var addrByDog = new BeContractCall()
            {
                Id = "GetAddressByDogId",
                Inputs = new Dictionary<string, dynamic>()
                {
                    { "MyDogID", dogId },
                }
            };

            // Another call will be made (to the API of the Proxy)
            var res = cm.Call(addrByDog);

            return res.Outputs.FirstOrDefault().Value;
        }

        /// <summary>
        /// Calls the IS to get it's info (mock)
        /// </summary>
        /// <returns>The contract return containing the IS info</returns>
        [HttpGet]
        [Route("serviceinfo")]
        public string GetServiceInfo()
        {
            // Calling the IS to get their info
            var res =  new BeContractReturn()
            {
                Id = "GetServiceInfo",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Name", "MockADS"},
                    { "Purpose", "Testing"},
                    { "CreationDate", DateTime.Now.ToShortDateString() }
                }
            };

            return JsonConvert.SerializeObject(res);
        }
    }
}
