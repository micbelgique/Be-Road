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
    public class MockZippopotamService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ps.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/BE/{eid.AddressPostal}");
                var response = "";
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
                //returning the employee list to view  
                return new MockPSDZippopotam() { Content = response };
            }
        }
    }
}