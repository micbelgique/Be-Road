using System;
using System.Collections.Generic;
using Contracts.Dal;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contracts.Logic;
using Contracts;

namespace ContractsTest
{
    [TestClass]
    public class AdapterServerTest
    {
        #region Initializing
        private AdapterServerService asService;
        private BeContractCall call;
        private Validators valid;

        public string GetBeContractCallString()
        {
            return @"{
            'Id': 'GetOwnerIdByDogId',
	        'Inputs': {
                    'DogID': 'D-123'
                }
            }";
        }

        [TestInitialize]
        public void TestInitialize()
        {
            asService = new AdapterServerService
            {
                ADSList = ASSMock.Fill()
            };

            valid = new Validators();
            call = valid.Generators.GenerateBeContractCall(GetBeContractCallString());
        }
        #endregion

        [TestMethod]
        public void TestCallPass()
        {
            var returnsActual = asService.Call(call);
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
        [ExpectedException(typeof(BeContractException))]
        public void TestCallFail()
        {
            call.Id = "Fail";
            var returnsActual = asService.Call(call);
        }
    }
}
