using System;
using System.Linq;
using Contracts.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContractsTest
{
    [TestClass]
    public class BeContractDalTest
    {
        public ContractContext Db { get; set; }
        public BeContractEqualsTest BeContractEquals { get; set; }

        [TestInitialize]
        public void Init()
        {
            //Used for connectionstring
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            BeContractEquals = new BeContractEqualsTest();
            Db = new ContractContext();
            Db.Contracts.RemoveRange(Db.Contracts);
        }

        [TestMethod]
        public void TestRemoveAll()
        {
            Db.Contracts.RemoveRange(Db.Contracts);
            Assert.AreEqual(0, Db.Contracts.Count());
            Assert.AreEqual(0, Db.Inputs.Count());
            Assert.AreEqual(0, Db.Queries.Count());
            Assert.AreEqual(0, Db.Mappings.Count());
            Assert.AreEqual(0, Db.Outputs.Count());
        }
    }
}
