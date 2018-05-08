using Contracts;
using Contracts.Dal;
using Contracts.Models;
using Newtonsoft.Json;
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
        public async Task<AdapterServer> FindASAsync(string name)
        {
            AdapterServer ret = null;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://centralserver/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"central/adapterserver?name={name}");
                    if (response.IsSuccessStatusCode)
                    {
                        ret = await response.Content.ReadAsAsync<AdapterServer>();
                    }
                }
                catch (Exception ex)
                {
                    throw new BeContractException("Error with Central Service when finding the AdapterServer: " + ex.Message);
                }
            }
          
            return ret;
        }

        public async Task<BeContractReturn> CallAsync(AdapterServer ads, BeContractCall call)
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
