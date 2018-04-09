using CentralServer.Dal;
using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeRoadTest
{
    [TestClass]
    public class AdapterServerTest
    {
        #region Initializing
        private IAdapterServerService asService;
        private BeContractCall call;
        private Validators valid;

        public AdapterServerTest()
        {
            asService = new AdapterServerServiceMockImpl();
            valid = new Validators();
            call = valid.Generators.GenerateBeContractCall(GetBeContractCallString());
        }

        public string GetBeContractCallString()
        {
            return @"{
            'Id': 'GetOwnerIdByDogId',
	        'Inputs': {
                    'DogID': 'D-123'
                }
            }";
        }
        #endregion

        [TestMethod]
        public async Task TestCallPassAsync()
        {
            var returnsActual = await asService.CallAsync(call);
            var returnsExpected = new BeContractReturn()
            {
                Id = "GetOwnerIdByDogId",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "OwnerIDOfTheDog", "Wilson !"}
                }
            };
            CollectionAssert.AreEqual(returnsExpected.Outputs, returnsActual.Outputs);
            Assert.AreEqual(returnsExpected.Id, returnsActual.Id);
        }

        [TestMethod]
        public async Task TestCallFail()
        {
            call.Id = "Fail";
            await Assert.ThrowsExceptionAsync<BeContractException>(() => asService.CallAsync(call), "No service found for Fail");
        }

    }
}
