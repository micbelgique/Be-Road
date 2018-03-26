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
    }
}
