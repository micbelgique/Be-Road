using Contracts;
using Contracts.Dal;
using Contracts.Models;
using Newtonsoft.Json;
using Proxy.Dal.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Dal
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
                return await FindAsync(ads, call);
            }
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };
        }

        public async Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call)
        {
            BeContractReturn res = null;
            using (var client = new HttpClient())
            {
                //Use the ads.Url as baseaddress, don't send this to Be-Road !
                try
                {
                    client.BaseAddress = new Uri(ads.Url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(ads.Root + "/" + call.Id, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        res = await response.Content.ReadAsAsync<BeContractReturn>();
                    }
                }
                catch (Exception ex)
                {
                    throw new BeContractException("Cannot call the Adapter Server: " + ex.Message);
                }
            }

            return res;
        }
    }
}
