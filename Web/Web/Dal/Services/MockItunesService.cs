using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Models.PublicServiceData;

namespace Web.Dal.Services
{
    public class MockItunesService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ps.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"?term={eid.FirstName}&limit=10");
                MockPSDItunes itunes = null;
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    itunes = JsonConvert.DeserializeObject<MockPSDItunes>(response);
                }
                return itunes;
            }
        }
    }
}