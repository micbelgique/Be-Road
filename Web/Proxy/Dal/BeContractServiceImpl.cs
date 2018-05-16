using Contracts.Dal;
using Contracts.Models;
using System.Linq;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Contracts;
using Proxy.Helpers;

namespace Proxy.Dal
{
    public class BeContractServiceImpl : IBeContractService
    {
        public async Task<BeContract> FindBeContractByIdAsync(string id)
        {
            BeContract ret = await FindSingleBeContractByIdAsync(id);

            //Because we only get the contracts id's from the queries
            //We need to get the queries contracts objects from here
            if (ret?.Queries != null)
            {
                foreach (var q in ret?.Queries)
                {
                    q.Contract = await FindBeContractByIdAsync(q.Contract.Id);
                }
            }

            return ret;
        }

        private async Task<BeContract> FindSingleBeContractByIdAsync(string id)
        {
            BeContract ret = null;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(ConfigHelper.GetServiceUrl("centralserver"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/central/contracts?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        ret = await response.Content.ReadAsAsync<BeContract>();
                    }
                }
                catch(Exception ex)
                {
                    throw new BeContractException("Error with Central Service when finding the BeContract: " + ex.Message);
                }
            }

            return ret;
        }
    }
}