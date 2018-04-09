using System;
using System.Collections.Generic;
using CentralServer.Dal;
using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeRoadTest
{
    [TestClass]
    public class CentralServerTest
    {
        #region Initialize
        private BeContractCall call;
        private Validators valid;

        public CentralServerTest()
        {
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
        public void TestCSPass()
        {
            var mockActual = CentralServerManager.FindMock(new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" }, call);
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
            Assert.ThrowsException<BeContractException>(() => CentralServerManager.FindMock(new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Fail", Url = "www.doggies.com/api/" }, call), "Unauthorized access for Doggies");
        }
    }
}
