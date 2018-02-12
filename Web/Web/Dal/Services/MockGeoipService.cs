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
    public class MockGeoipService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ps.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("");
                var response = "";
                MockPSDGeoip geoip = null;
                if (Res.IsSuccessStatusCode)
                {
                    response = Res.Content.ReadAsStringAsync().Result;
                    geoip = JsonConvert.DeserializeObject<MockPSDGeoip>(response);
                }
                return geoip;
            }
        }
    }
}