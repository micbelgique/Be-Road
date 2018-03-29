using System;
using Contracts.Dal.Mock;
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
            var ads = asService.FindAS("GetOwnerIdByDogId");
            Assert.AreEqual(new AdapterServer() { ContractNames = { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" }, ads);
        }

        [TestMethod]
        public void TestFindASFail()
        {
            var ads = asService.FindAS("Fail");
            Assert.AreEqual(new AdapterServer() { ContractNames = { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" }, ads);
        }
    }
}
