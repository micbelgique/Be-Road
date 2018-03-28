using System;
using System.Collections.Generic;
using Contracts;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContractsTest
{
    [TestClass]
    public class BeContractCallTest
    {
        #region Initializing
        public Validators Validators { get; set; }

        public BeContractCallTest()
        {
            Validators = new Validators();
        }

        public BeContract CreateGoodContract()
        {
            return BeContractsMock.GetOwnerIdByDogId();
        }

        public string GetBeContractCallString()
        {
            return @"{
            'Id': 'GetOwnerIdByDogId',
	        'Inputs': {
                    'DogID': 'EG-673KL'
                }
            }";
        }

        public BeContractCall GetBeContractCall(string str)
        {
            return Validators.Generators.GenerateBeContractCall(str);
        }

        public BeContractCall GetBeContractCall()
        {
            return GetBeContractCall(GetBeContractCallString());
        }

        #endregion


        [TestMethod]
        public void TestGetBeContractFromJsonWorking()
        {
            var call = Validators.Generators.GenerateBeContractCall(GetBeContractCallString());
            Assert.AreEqual("GetOwnerIdByDogId", call.Id);
            Assert.AreEqual("EG-673KL", call.Inputs["DogID"] as string);
        }

        [TestMethod]
        public void TestGetBeContractFromJsonWithoutIdFail()
        {
            var json =  @"{
	        'Inputs': {
                    'DogID': 'EG-673KL'
                }
            }";
            try
            {
                Validators.Generators.GenerateBeContractCall(json);
                Assert.Fail("Contract should not be valid without an Id");
            } catch(BeContractException) {}
        }

        [TestMethod]
        public void TestValidateBeContractCallWithBadIdFail()
        {
            var contract = CreateGoodContract();
            var call = GetBeContractCall();
            call.Id = "GetDogowner";
            try
            {
                Validators.ValidateBeContractCall(contract, call);
                Assert.Fail("Contract's do not have the same ID, must throw an exception");
            }
            catch (BeContractException ex)
            {
                Assert.AreEqual("Contract's do not have the same ID", ex.Message);
            }
        }

        [TestMethod]
        public void TestValidateBeContractCallWithBadTypesFail()
        {
            var contract = CreateGoodContract();
            var call = GetBeContractCall();
            call.Inputs["DogID"] = 54;
            try
            {
                Validators.ValidateBeContractCall(contract, call);
                Assert.Fail("Contract's do not have the input types, must throw an exception");
            }
            catch (BeContractException ex)
            {
                Assert.AreEqual($"The contract expects {contract.Inputs[0].Type.Name} but {call.Inputs["DogID"].GetType().Name} was found", ex.Message);
            }
        }
        
        [TestMethod]
        public void TestValidateBeContractCallWithEmptyInputRequiredFail()
        {
            var contract = CreateGoodContract();
            var call = GetBeContractCall();
            call.Inputs.Remove("DogID");
            try
            {
                Validators.ValidateBeContractCall(contract, call);
                Assert.Fail("An required empty input, must throw an exception");
            }
            catch (BeContractException ex)
            {
                Assert.AreEqual($"No key was found for DogID and it is required", ex.Message);
            }
        }
        
        [TestMethod]
        public void TestValidateBeContractCallWithEmptyInputNotRequiredWorking()
        {
            var contract = BeContractsMock.GetDoubleInputContract();
            var callString = @"{
            'Id': 'DoubleInputContract',
	        'Inputs': {
                    'First': 1090
                }
            }";
            var call = GetBeContractCall(callString);
            Validators.ValidateBeContractCall(contract, call);
        }

        [TestMethod]
        public void TestValidateBeContractCallWithAllWorking()
        {
            var contract = CreateGoodContract();
            var call = GetBeContractCall();
            Validators.ValidateBeContractCall(contract, call);
        }

    }
}
