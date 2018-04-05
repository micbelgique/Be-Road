using System.Linq;
using System.Collections.Generic;
using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proxy;
using Contracts.Dal.Mock;

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
            cm = new ContractManager(ass = new AdapterServerService());
            ass.SetADSList(ASSMock.Fill());

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
            var ads = ass.GetADSList().FirstOrDefault(a => a.ISName.Equals("MathLovers"));
            ass.GetADSList().Remove(ads);
            var ex = Assert.ThrowsException<BeContractException>(() => cm.Call(mathCall));
            Assert.AreEqual("No service found for GetMathemathicFunction", ex.Message);
            ass.GetADSList().Add(ads);
        }

        [TestMethod]
        public void TestContractIsNullWillThrowException()
        {
            var id = mathCall.Id;
            mathCall.Id = "UnexistingId";
            var ex = Assert.ThrowsException<BeContractException>(() => cm.Call(mathCall));
            Assert.AreEqual("No contract was found with id UnexistingId", ex.Message);
            mathCall.Id = id;
        }

        [TestMethod]
        public void TestContractCallIsNullWillThrowException()
        {
            var ex = Assert.ThrowsException<BeContractException>(() => cm.Call(null));
            Assert.AreEqual("Contract call is null", ex.Message);
        }
    }
}
