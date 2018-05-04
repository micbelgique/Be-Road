using Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Dto;

namespace Web.Dal
{
    public class PSDataService
    {
        public async Task<List<PublicServiceData>> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            var lst = new List<PublicServiceData>();
            ps.ContractNames = await GetContractNames(ps);
            foreach(var contractName in ps.ContractNames)
            {
                lst.Add(await GetPSDOfContract(eid.RNN, contractName));
            }
            return lst;
        }

        private async Task<PublicServiceData> GetPSDOfContract(string nrid, string contractId)
        {
            var data = new PublicServiceData();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var call = new BeContractCall
                    {
                        Id = contractId,
                        ISName = "Privacy Passport",
                        Inputs = new Dictionary<string, dynamic>()
                        {
                            { "NRID", nrid },
                            { "Justification", "Reading my own data"}
                        }
                    };
                    var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsAsync<Dictionary<int, BeContractReturn>>();
                        var outputs = resp.Values.Select(ret => ret.Outputs);
                        data.NRID = nrid;
                        data.Datas = outputs.SelectMany(d => d)
                                            .ToDictionary(t => t.Key, t => t.Value);
                        data.AccessInfos = await GetAccessInfosOf(contractId, nrid);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return data;
        }

        private async Task<List<String>> GetContractNames(PublicService ps)
        {
            using (var client = new HttpClient())
            {
                var lst = new List<String>();
                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/contract/ads?isName={ps.Name}");
                    if (response.IsSuccessStatusCode)
                    {
                        lst = await response.Content.ReadAsAsync<List<String>>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lst;
            }
        }

        private async Task<List<AccessInfoDto>> GetAccessInfosOf(string contractId, string nrid)
        {
            using (var client = new HttpClient())
            {
                var lst = new List<AccessInfoDto>();
                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/contract/justification?contractId={contractId}&nrid={nrid}");
                    if (response.IsSuccessStatusCode)
                    {
                        lst = await response.Content.ReadAsAsync<List<AccessInfoDto>>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lst;
            }
        }
    }
}