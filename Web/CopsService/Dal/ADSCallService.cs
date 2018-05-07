using Contracts.Models;
using Newtonsoft.Json;
using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PublicService.Dal
{
    public class ADSCallService
    {
        private async Task<ApplicationUser> GetInfosOf(string nrid)
        {
            using (var client = new HttpClient())
            {
                var user = new ApplicationUser();

                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var call = new BeContractCall
                    {
                        Id = "GetFunnyContract",
                        ISName = $"Public Service/Administrator",
                        Inputs = new Dictionary<string, dynamic>()
                        {
                            { "NRID", nrid },
                            { "Justification", "Updating PS database"}
                        }
                    };

                    var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsAsync<Dictionary<int, BeContractReturn>>();
                        var outputs = resp.Values.Select(ret => ret.Outputs);
                        user.UserName = nrid;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return user;
            }
        }
    }
}