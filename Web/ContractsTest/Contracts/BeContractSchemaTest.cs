using Contracts;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BeRoadTest.Contracts
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
        public async Task TestValidateBeContractWithoutQueryMappingContractKeyFail()
        {
            try
            {
                var contract = BeContractsMock.GetAddressByDogId();
                contract.Queries[0].Mappings[0].LookupInputKey = null;

                await Validators.ValidateBeContract(contract);
                Assert.Fail("Contract should not be valid without an query mapping contractkey");
            }
            catch (BeContractException ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        [TestMethod]
        public async Task TestValidateBeContractWithQueryMappingLookupInputKeySuccess()
        {
            var contract = BeContractsMock.GetAddressByDogId();
            await Validators.ValidateBeContract(contract);
        }

        [TestMethod]
        public async Task TestValidateBeContractWithDuplicatedInputsSuccess()
        {
            var contract = BeContractsMock.GetMathemathicFunction();
            Assert.IsTrue(await Validators.ValidateBeContract(contract));
        }

        [TestMethod]
        public async Task TestValidateBeContractWithDuplicatedInputsFailAsync()
        {
            var contract = BeContractsMock.GetMathemathicFunction();
            contract.Inputs[0].Key = "B";
            try
            {
                await Validators.ValidateBeContract(contract);
            }
            catch(BeContractException ex)
            {
                var exc = new BeContractException("Duplicated key in GetMathemathicFunction contract for Inputs B, B");
                Assert.AreEqual(ex.Message, exc.Message);
            }
        }
    }
}
