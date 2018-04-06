using Contracts.Dal;
using Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractsTest
{
    /// <summary>
    /// Class used to mock the Adapter Server Service
    /// </summary>
    public class ASSMock : AdapterServerService
    {
        public ASSMock()
        {
            ADSList = new List<AdapterServer>
            {
                new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" },
                new AdapterServer() { ContractNames = new List<string>  { "GetMathemathicFunction" }, ISName = "MathLovers", Url = "www.mathlovers.com/api/" },
                new AdapterServer() { ContractNames = new List<string>  { "GetAddressByOwnerId" }, ISName = "CitizenDatabank", Url = "www.citizens.com/api/" },
            };
        }
        public new async Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call)
        {
            System.Console.WriteLine("Mocking this task");
            /*BeContractReturn res = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53369/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                string json = JsonConvert.SerializeObject(new ASFindRequest()
                {
                    Ads = ads,
                    Call = call
                });
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/central/find", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    res = await response.Content.ReadAsAsync<BeContractReturn>();
                }
            }*/

            return null;
        }
    }
}
