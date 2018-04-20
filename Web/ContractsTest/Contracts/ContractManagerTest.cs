using BeRoadTest.ADSMock;
using Contracts;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeRoadTest.Contracts
{
    [TestClass]
    public class ContractManagerTest
    {
        private ContractManager cm;
        private AdapterServerServiceMockImpl ass;
        private BeContractCall mathCall, adrByDog;

        public ContractManagerTest()
        {
            //TODO: init the ads
            cm = new ContractManager()
            {
                AsService = ass = new AdapterServerServiceMockImpl(),
                BcService = new BeContractServiceImpl()
            };

            mathCall = new BeContractCall()
            {
                Id = "GetMathemathicFunction",
                Inputs = new Dictionary<string, dynamic>()
                {
                    { "A", 54 },
                    { "B", 154 }
                }
            };

            adrByDog = new BeContractCall()
            {
                Id = "GetAddressByDogId",
                Inputs = new Dictionary<string, dynamic>()
                {
                    { "MyDogID", "Heyto" },
                }
            };
        }

        [TestMethod]
        public async Task TestNoServiceFoundWillThrowException()
        {
            var ads = ass.ADSList.FirstOrDefault(a => a.ISName.Equals("MathLovers"));
            ass.ADSList.Remove(ads);
            var ex = await Assert.ThrowsExceptionAsync<BeContractException>(() => cm.CallAsync(mathCall));
            Assert.AreEqual("No service found for GetMathemathicFunction", ex.Message);
            ass.ADSList.Add(ads);
        }

        [TestMethod]
        public async Task TestContractIsNullWillThrowException()
        {
            var id = mathCall.Id;
            mathCall.Id = "UnexistingId";
            var ex = await Assert.ThrowsExceptionAsync<BeContractException>(() => cm.CallAsync(mathCall));
            Assert.AreEqual("No contract was found with id UnexistingId", ex.Message);
            mathCall.Id = id;
        }

        [TestMethod]
        public async Task TestContractCallIsNullWillThrowException()
        {
            var ex = await Assert.ThrowsExceptionAsync<BeContractException>(() => cm.CallAsync(null));
            Assert.AreEqual("Contract call is null", ex.Message);
        }
    }
}
