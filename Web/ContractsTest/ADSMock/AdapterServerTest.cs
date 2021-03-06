﻿using CentralServer.Dal;
using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeRoadTest.ADSMock
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
            'ISName': 'Test man',
	        'Inputs': {
                    'DogID': 'D-123'
                }
            }";
        }
        #endregion

        [TestMethod]
        public async Task TestCallPassAsync()
        {
            var ads = await asService.FindASAsync(call.ISName);
            var returnsActual = await asService.CallAsync(ads, call);
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
        

    }
}
