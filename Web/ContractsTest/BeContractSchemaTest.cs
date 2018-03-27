using System;
using System.Collections.Generic;
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
            var GetDogOwnerContract = new BeContract()
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

            GetDogOwnerContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    Contract = GetDogOwnerContract,
                    Key = "OwnerID",
                    Description = "The ID of the owner of the dog",
                    Type = typeof(string)
                }
            };
            return GetDogOwnerContract;
        }

        #endregion

        [TestMethod]
        public void TestValidateBeContractWorking()
        {
            Assert.Fail("JSchema library must change");
            //Assert.IsTrue(Validators.ValidateBeContract(CreateGoodContract()));
        }

        [TestMethod]
        public void TestValidateBeContractWithoutIdFail()
        {
            try
            {
                Validators.ValidateBeContract(new BeContract()
                {
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
                        }
                    }
                });
                Assert.Fail("Contract should not be valid without an Id");
            }
            catch (BeContractException) {}
        }

        [TestMethod]
        public void TestValidateBeContractWithoutInputKeyFail()
        {
            try
            {
                Validators.ValidateBeContract(new BeContract()
                {
                    Id = "GetOwnerIdByDogId",
                    Description = "This contract is used to get the dog owner",
                    Version = "V001",
                    Inputs = new List<Input>()
                    {
                        new Input()
                        {
                            Description = "The ID of the Dog",
                            Required = true,
                            Type = typeof(string)
                        }
                    }
                });
                Assert.Fail("Contract should not be valid without an Id");
            }
            catch (BeContractException) { }
        }

        [TestMethod]
        public void TestValidateBeContractWithoutInputTypeFail()
        {
            try
            {
                Validators.ValidateBeContract(new BeContract()
                {
                    Id = "GetOwnerIdByDogId",
                    Description = "This contract is used to get the dog owner",
                    Version = "V001",
                    Inputs = new List<Input>()
                    {
                        new Input()
                        {
                            Key = "DogId",
                            Description = "The ID of the Dog",
                            Required = true,
                        }
                    }
                });
                Assert.Fail("Contract should not be valid without an Id");
            }
            catch (BeContractException) { }
        }

    }
}
