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

        #endregion

        [TestMethod]
        public void ValidateBeContractWorking()
        {
            Assert.IsTrue(Validators.ValidateBeContract(CreateGoodContract()));
        }

        [TestMethod]
        public void ValidateBeContractWithoutId()
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
        public void ValidateBeContractWithoutInputKey()
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
        public void ValidateBeContractWithoutInputType()
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
