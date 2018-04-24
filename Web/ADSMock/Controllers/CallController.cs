using Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<Dictionary<int, BeContractReturn>> GetOwnerByDogAsync(string dogId) {
            var ownerByDog = new BeContractCall()
            {
                Id = "GetOwnerIdByDogId",
                Inputs = new Dictionary<string, dynamic>()
                {
                    { "DogID", dogId },
                }
            };
            
            return await CallToProxyAsync(ownerByDog);
        }

        /// <summary>
        /// Calls Be-Road to find the dog address
        /// </summary>
        /// <param name="dogId">The ID of the dog</param>
        /// <returns>The dog address</returns>
        [HttpGet]
        [Route("addressbydog/{dogid}")]
        public async Task<Dictionary<int, BeContractReturn>> GetAddressByDogAsync(string dogId)
        {
            var addrByDog = new BeContractCall()
            {
                Id = "GetAddressByDogId",
                Inputs = new Dictionary<string, dynamic>()
                {
                    { "MyDogID", dogId },
                }
            };

            return await CallToProxyAsync(addrByDog);
        }

        private async Task<Dictionary<int, BeContractReturn>> CallToProxyAsync(BeContractCall call)
        {
            Dictionary<int, BeContractReturn> ret = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://proxy/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    ret = await response.Content.ReadAsAsync<Dictionary<int, BeContractReturn>>();
                }
            }
            return ret;
        }
        
    }
}
