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
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            var user = new PublicServiceData();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var call = new BeContractCall
                    {
                        Id = ps.ContractId,
                        ISName = "Privacy Passport",
                        Inputs = new Dictionary<string, dynamic>()
                        {
                            { "NRID", eid.RNN },
                            { "Justification", "Reading my own data"}
                        }
                    };
                    var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
                    if(response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsAsync<Dictionary<int, BeContractReturn>>();
                        var outputs = data.Values.Select(ret => ret.Outputs);
                        user.NRID = eid.RNN;
                        user.Datas = outputs.SelectMany(d => d)
                                            .ToDictionary(t => t.Key, t => t.Value);
                        user.AccessInfos = await GetAccessInfosOf(ps.ContractId, eid.RNN);
                        user.AccessInfos = user.AccessInfos.Where(a => !a.Name.Equals("Privacy Passport")).ToList();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return user;
        }

        private async Task<List<AccessInfoDto>> GetAccessInfosOf(string contractId, string nrid)
        {
            var lst = new List<AccessInfoDto>();
            using (var client = new HttpClient())
            {
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
            }
            return lst;
        }
    }
}