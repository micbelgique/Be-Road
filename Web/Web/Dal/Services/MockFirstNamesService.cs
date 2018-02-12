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
                var response = "";
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var json = JObject.Parse(Res.Content.ReadAsStringAsync().Result);
                    response = json["records"].ToString();                    
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
                //returning the employee list to view  
                return new MockPSDFirstNames() { Content = response };
            }
        }
    }
}