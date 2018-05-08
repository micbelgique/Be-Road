using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeRoadTest.Proxy
{
    [TestClass]
    public class ProxyTest
    {
        //static async Task RunAsync(string expectedId)
        //{
        //    string product = null;
        //    BeContractReturn res = null;
        //    var call = new BeContractCall()
        //    {
        //        Id = "GetAddressByDogId",
        //        Inputs = new Dictionary<string, dynamic>() {
        //                { "MyDogID", "D-123" }
        //        }
        //    };
        //    var expected = new BeContractReturn()
        //    {
        //        Id = expectedId,
        //        Outputs = new Dictionary<string, dynamic>()
        //        {
        //            { "Street", "Charleroi nord"},
        //            { "StreetNumber", 9999},
        //            { "Country", "Belgique" }
        //        }
        //    };

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:52831/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

        //        string json = await Task.Run(() => JsonConvert.SerializeObject(call));
        //        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            product = await response.Content.ReadAsAsync<string>();
        //            res = JsonConvert.DeserializeObject<BeContractReturn>(product);
        //        }
        //    }

        //    Assert.AreEqual(expected.Id, res.Id);
        //    CollectionAssert.AreEqual(expected.Outputs, res.Outputs);
        //}

        //[TestMethod]
        //public void TestCallContractSuccess()
        //{
        //    RunAsync("GetAddressByDogId").Wait();
        //}

        //[TestMethod]
        //public void TestCallContractWrongIdFail()
        //{
        //    RunAsync("Fail").Wait();
        //}
    }
}
