using Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal
{
    /// <summary>
    /// Class used to find an adapter server
    /// </summary>
    public class AdapterServerService
    {
        private List<AdapterServer> aDSList;

        /// <summary>
        /// List of the adapter servers
        /// </summary>
        public List<AdapterServer> GetADSList()
        {
            return aDSList;
        }

        /// <summary>
        /// List of the adapter servers
        /// </summary>
        public void SetADSList(List<AdapterServer> value)
        {
            aDSList = value;
        }

        /// <summary>
        /// Find an adapter server with the name of the contract used
        /// </summary>
        /// <param name="name">The name of the contract used</param>
        /// <returns>The found adapter server</returns>
        private AdapterServer FindAS(string name)
        {
            return GetADSList().FirstOrDefault(s => s.ContractNames.Any(cn => cn.Equals(name)));
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
                return FindAsync(ads, call).GetAwaiter().GetResult();
            }
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };
        }

        static async Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call)
        {
            string product = null;
            BeContractReturn res = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53369/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                string json = JsonConvert.SerializeObject(new ASFindRequest() {
                    Ads = ads,
                    Call = call
                });
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/central/find", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<string>();
                    res = JsonConvert.DeserializeObject<BeContractReturn>(product);
                }
            }

            return res;
        }
    }
}
