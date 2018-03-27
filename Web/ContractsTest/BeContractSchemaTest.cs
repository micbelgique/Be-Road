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
                },
                //Query
                Query = new List<BeContract>(){
                    new BeContract()
                    {
                        Id = "GetCitizenIDfromDogID",
                        Description = "This contract is used to get the dog owner ID",
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
                    }
                    //db.Contracts.FindById("GetCitizenIDfromDogID")
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
        public async Task TestValidateBeContractWorking()
        {
            Assert.IsTrue(await Validators.ValidateBeContract(CreateGoodContract()));
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutIdFail()
        {
            try
            {
                await Validators.ValidateBeContract(new BeContract()
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
                    },
                    //Query
                    Query = new List<BeContract>(){
                        new BeContract()
                        {
                            Id = "GetCitizenIDfromDogID",
                            Description = "This contract is used to get the dog owner ID",
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
                        }
                        //db.Contracts.FindById("GetCitizenIDfromDogID")
                    }
                });
                Assert.Fail("Contract should not be valid without an Id");
            }
            catch (BeContractException) {}
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutInputKeyFail()
        {
            try
            {
                await Validators.ValidateBeContract(new BeContract()
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
                    },
                    //Query
                    Query = new List<BeContract>(){
                        new BeContract()
                        {
                            Id = "GetCitizenIDfromDogID",
                            Description = "This contract is used to get the dog owner ID",
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
                        }
                        //db.Contracts.FindById("GetCitizenIDfromDogID")
                    }
                });
                Assert.Fail("Contract should not be valid without an Id");
            }
            catch (BeContractException) { }
        }

        [TestMethod]
        public async Task TestValidateBeContractWithoutInputTypeFail()
        {
            try
            {
                await Validators.ValidateBeContract(new BeContract()
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
                    },
                    //Query
                    Query = new List<BeContract>(){
                        new BeContract()
                        {
                            Id = "GetCitizenIDfromDogID",
                            Description = "This contract is used to get the dog owner ID",
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
                        }
                        //db.Contracts.FindById("GetCitizenIDfromDogID")
                    }
                });
                Assert.Fail("Contract should not be valid without an Id");
            }
            catch (BeContractException) { }
        }

    }
}
