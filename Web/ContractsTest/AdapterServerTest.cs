using System;
using System.Collections.Generic;
using Contracts.Dal;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContractsTest
{
    [TestClass]
    public class AdapterServerTest
    {
        #region Initializing
        private AdapterServerService asService;

        [TestInitialize]
        public void TestInitialize()
        {
            asService = new AdapterServerService
            {
                ADSList = ASSMock.Fill()
            };
        }
        #endregion

        [TestMethod]
        public void TestFindASPass()
        {
            var adsActual = asService.FindAS("GetOwnerIdByDogId");
            var adsExpected = new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" };
            CollectionAssert.AreEqual(adsExpected.ContractNames, adsActual.ContractNames);
            Assert.AreEqual(adsExpected.ISName, adsActual.ISName);
            Assert.AreEqual(adsExpected.Url, adsActual.Url);

        }

        [TestMethod]
        public void TestFindASFail()
        {
            var adsActual = asService.FindAS("Fail");
            Assert.AreEqual(null, adsActual);
        }
    }
}
