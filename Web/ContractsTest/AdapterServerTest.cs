using System;
using System.Collections.Generic;
using Contracts.Dal;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contracts.Logic;
using Contracts;
using Contracts.Dal.Mock;

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
        public void TestCallFail()
        {
            call.Id = "Fail";
            Assert.ThrowsException<BeContractException>(() => asService.Call(call), "No service found for Fail");
        }

        [TestMethod]
        public void TestCSPass()
        {
            var mockActual = CentralServer.FindMock(new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" }, call);
            var mockExpected = new BeContractReturn()
            {
                Id = "GetOwnerIdByDogId",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "OwnerIDOfTheDog", "Wilson !"}
                }
            };
            CollectionAssert.AreEqual(mockExpected.Outputs, mockActual.Outputs);
            Assert.AreEqual(mockExpected.Id, mockActual.Id);
        }

        [TestMethod]
        public void TestCSFail()
        {
            Assert.ThrowsException<BeContractException>(() => CentralServer.FindMock(new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Fail", Url = "www.doggies.com/api/" }, call), "Unauthorized access for Doggies");
        }
    }
}
