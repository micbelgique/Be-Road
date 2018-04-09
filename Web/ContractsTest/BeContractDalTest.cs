﻿using System;
using System.Linq;
using Contracts.Dal;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeRoadTest
{
    [TestClass]
    public class BeContractDalTest
    {
        public ContractContext Db { get; set; }
        public BeContractEqualsTest BeContractEquals { get; set; }
        
        public BeContractDalTest()
        {
            BeContractEquals = new BeContractEqualsTest();
            Db = new ContractContext();
        }

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            //Used for connectionstring
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            var db = new ContractContext();
            db.Contracts.RemoveRange(db.Contracts);
            db.SaveChanges();
        }

        [TestMethod]
        public void TestRemoveAll()
        {
            Db.Contracts.RemoveRange(Db.Contracts);
            Db.SaveChanges();
            Assert.AreEqual(0, Db.Contracts.Count());
            Assert.AreEqual(0, Db.Inputs.Count());
            Assert.AreEqual(0, Db.Queries.Count());
            Assert.AreEqual(0, Db.Mappings.Count());
            Assert.AreEqual(0, Db.Outputs.Count());
        }

        public void TestAddAndRead(BeContract contract)
        {
            Db.Contracts.Add(contract);
            Db.SaveChanges();
            var owner = Db.Contracts.FirstOrDefault(c => c.Id.Equals(contract.Id));
            BeContractEquals.AreEquals(contract, owner);
        }

        [TestMethod]
        public void TestAddAndReadDogOwner()
        {
            TestAddAndRead(BeContractsMock.GetOwnerIdByDogId());
        }

        [TestMethod]
        public void TestAddAndReadDoubleInputsWithoutQuery()
        {
            TestAddAndRead(BeContractsMock.GetDoubleInputContract());
        }

        [TestMethod]
        public void TestAddAndReadWithQuery()
        {
            TestAddAndRead(BeContractsMock.GetAddressByDogId());
        }

        [TestMethod]
        public void TestRemoveAllAndAddAll()
        {
            TestRemoveAll();
            TestAddAndReadDogOwner();
            TestAddAndReadDoubleInputsWithoutQuery();
            TestAddAndReadWithQuery();
            TestRemoveAll();
        }

    }
}
