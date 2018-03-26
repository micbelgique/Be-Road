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
            return new BeContract()
            {
                Id = "GetDogOwner",
                Description = "This contract is used to get the dog owner",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "DogID",
                        Description = "The ID of the Dog",
                        Required = true,
                        Type = typeof(string)
                    },
                    new Input()
                    {
                        Key = "Age",
                        Description = "Age of the dog",
                        Required = false,
                        Type = typeof(int)
                    },
                }
            };
        }

        public string GetBeContractCallString()
        {
            return @"{
            'Id': 'GetDogOwner',
	        'Inputs': {
                    'DogID': 'EG-673KL',
                    'Age': 54
                }
            }";
        }

        public BeContractCall GetBeContractCall()
        {
            return Validators.Generators.GenerateBeContractCall(GetBeContractCallString());
        }

        #endregion


        [TestMethod]
        public void TestGetBeContractFromJsonWorking()
        {
            var call = Validators.Generators.GenerateBeContractCall(GetBeContractCallString());
            Assert.AreEqual("GetDogOwner", call.Id);
            Assert.AreEqual("EG-673KL", call.Inputs["DogID"] as string);
            Assert.AreEqual(54, (int)call.Inputs["Age"]);
        }

        [TestMethod]
        public void TestGetBeContractFromJsonWithoutIdFail()
        {
            var json =  @"{
	        'Inputs': {
                    'DogID': 'EG-673KL',
                    'Age': 54
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
            call.Inputs["Age"] = "54";
            try
            {
                Validators.ValidateBeContractCall(contract, call);
                Assert.Fail("Contract's do not have the input types, must throw an exception");
            }
            catch (BeContractException ex)
            {
                Assert.AreEqual($"The contract expects {contract.Inputs[1].Type.Name} but {call.Inputs["Age"].GetType().Name} was found", ex.Message);
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
        public void TestValidateBeContractCallWithEmptyInputNptRequiredWorking()
        {
            var contract = CreateGoodContract();
            var call = GetBeContractCall();
            call.Inputs.Remove("Age");
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
