using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ContractsTest
{
    [TestClass]
    public class ContractManagerTest
    {
        private ContractManager cm;
        private AdapterServerService ass;
        private BeContractCall mathCall, adrByDog;

        [TestInitialize]
        public void Init()
        {
            //TODO: init the ads
            cm = new ContractManager()
            {
                AsService = new AdapterServerService()
                {
                    ADSList = ASSMock.Fill()
                },
                BcService = new BeContractService()
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
        public void TestNoServiceFoundWillThrowException()
        {
            var ads = ass.ADSList.FirstOrDefault(a => a.ISName.Equals("MathLovers"));
            ass.ADSList.Remove(ads);
            var ex = Assert.ThrowsException<BeContractException>(async () => await cm.CallAsync(mathCall));
            Assert.AreEqual("No service found for GetMathemathicFunction", ex.Message);
            ass.ADSList.Add(ads);
        }

        [TestMethod]
        public void TestContractIsNullWillThrowException()
        {
            var id = mathCall.Id;
            mathCall.Id = "UnexistingId";
            var ex = Assert.ThrowsException<BeContractException>(async () => await cm.CallAsync(mathCall));
            Assert.AreEqual("No contract was found with id UnexistingId", ex.Message);
            mathCall.Id = id;
        }

        [TestMethod]
        public void TestContractCallIsNullWillThrowException()
        {
            var ex = Assert.ThrowsException<BeContractException>(async () => await cm.CallAsync(null));
            Assert.AreEqual("Contract call is null", ex.Message);
        }
    }
}
