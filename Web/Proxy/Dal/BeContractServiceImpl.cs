using Contracts.Dal;
using Contracts.Models;
using System.Linq;
using Proxy.Dal.Mock;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Proxy.Dal
{
    public class BeContractServiceImpl : IBeContractService
    {
        public async Task<BeContract> FindBeContractByIdAsync(string id)
        {
            BeContract ret = await FindSingleBeContractByIdAsync(id);
            //Because we only get the contracts id's from the queries
            //We need to get the queries contracts objects from here
      
            //Does it works for recursive contracts ?
            foreach (var q in ret.Queries)
            {
                q.Contract = await FindBeContractByIdAsync(q.Contract.Id);
                //q.Contract = await FindSingleBeContractByIdAsync(q.Contract.Id);
            }

            return ret;
        }

        private async Task<BeContract> FindSingleBeContractByIdAsync(string id)
        {
            BeContract ret = null;
            using (var client = new HttpClient())
            {
                //Use docker container name !
                client.BaseAddress = new Uri("http://localhost:53369/api/central/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"contracts?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    ret = await response.Content.ReadAsAsync<BeContract>();
                }
            }

            return ret;
        }
    }
}