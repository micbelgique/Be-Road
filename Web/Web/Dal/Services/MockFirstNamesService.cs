using Newtonsoft.Json.Linq;
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
    public class MockFirstNamesService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ps.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"?dataset=prenoms-masculins-20150&refine.prenom={eid.FirstName}");
                MockPSDFirstNames names = null;
                if (Res.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(Res.Content.ReadAsStringAsync().Result);
                    names = new MockPSDFirstNames() { Records = new List<MockPSDRecord>() };
                    foreach(var obj in json["records"])
                    {
                        var fields = obj["fields"];
                        names.Records.Add(new MockPSDRecord()
                        {
                            Count = Convert.ToInt32(fields["nombre"].ToString()),
                            SearchedName = fields["prenom"].ToString()
                        });
                    }
                }
                return names;
            }
        }
    }
}