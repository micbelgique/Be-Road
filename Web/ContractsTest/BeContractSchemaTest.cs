using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContractsTest
{
    [TestClass]
    public class BeContractSchemaTest
    {
        #region Initializing
        public Validators Validators { get; set; }
        
        public BeContractSchemaTest()
        {
            Validators = new Validators();
        }

        public BeContract CreateGoodContract()
        {
            return BeContractsMock.GetOwnerIdByDogId();
        }

        #endregion

        [TestMethod]
        public async Task TestValidateBeContractWorking()
        {
            Assert.IsTrue(await Validators.ValidateBeContract(CreateGoodContract()));
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutIdFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByOwnerId();
                contract.Id = null;
                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an Id");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutInputKeyFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByOwnerId();
                contract.Inputs[0].Key = null;
                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an input key");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutInputTypeFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByOwnerId();
                contract.Inputs[0].Type = null;
                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an input type");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutOutputTypeFail()
        {
            try
            {
                var contract = CreateGoodContract();
                contract.Outputs[0].Type = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an output type");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutOutputContractFail()
        {
            try
            {
                var contract = CreateGoodContract();
                contract.Outputs[0].Contract = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an output contract");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutOutputKeyFail()
        {
            try
            {
                var contract = CreateGoodContract();
                contract.Outputs[0].Key = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an output Key");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithQueryContractFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByDogId();
                contract.Queries[0].Contract = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an query contract");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithQueryMappingInputKeyFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByDogId();
                contract.Queries[0].Mappings[0].InputKey = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an query mapping input key");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        [TestMethod]
        public async Task TestValidateBeContractWithQueryMappingContractFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByDogId();
                contract.Queries[0].Mappings[0].Contract = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an query mapping contract");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        [TestMethod]
        public async Task TestValidateBeContractWithQueryMappingContractKeyFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByDogId();
                contract.Queries[0].Mappings[0].ContractKey = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an query mapping contractkey");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        [TestMethod]
        public async Task TestValidateBeContractWithoutQueryMappingContractKeyFail()
        {
            var contract = BeContractsMock.GetAddressByDogId();
            await Validators.ValidateBeContract(contract);
        }
    }
}
