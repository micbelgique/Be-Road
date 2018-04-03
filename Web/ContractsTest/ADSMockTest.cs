using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ContractsTest
{
    [TestClass]
    public class ADSMockTest
    {
        static async Task RunAsync(string id, string result, string url)
        {
            string product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59317/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                // New code:
                HttpResponseMessage response = await client.GetAsync($"api/call/{url}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<string>();
                }
            }

            Assert.AreEqual(result, product);
        }

        static async Task ServiceInfoAsync()
        {
            string product = null;
            BeContractReturn res = null;
            var expected = new BeContractReturn()
            {
                Id = "GetServiceInfo",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Name", "MockADS"},
                    { "Purpose", "Testing"},
                    { "CreationDate", DateTime.Now.ToShortDateString() }
                }
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59317/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                // New code:
                HttpResponseMessage response = await client.GetAsync("api/call/serviceinfo/");
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<string>();
                    res = JsonConvert.DeserializeObject<BeContractReturn>(product);
                }
            }
            
            expected.Outputs.TryGetValue("Name", out dynamic expectedName);
            res.Outputs.TryGetValue("Name", out dynamic actualName);

            expected.Outputs.TryGetValue("Purpose", out dynamic expectedPurp);
            res.Outputs.TryGetValue("Purpose", out dynamic actualPurp);

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedPurp, actualPurp);
        }

        [TestMethod]
        public void TestGetOwnerByDogSuccess()
        {
            RunAsync("D-123", "Wilson !", "ownerbydog").Wait();
        }

        [TestMethod]
        public void TestGetOwnerByDogWrongIdFail()
        {
            RunAsync("Fail", "Incognito !", "ownerbydog").Wait();
        }

        [TestMethod]
        public void TestGetAddressByDogSuccess()
        {
            RunAsync("D-123", "Charleroi nord", "addrbydog").Wait();
        }

        [TestMethod]
        public void TestGetAddressByDogWrongIdFail()
        {
            RunAsync("Fail", "SDF", "addrbydog").Wait();
        }

        [TestMethod]
        public void TestGetServiceInfo()
        {
            ServiceInfoAsync().Wait();
        }
    }
}
