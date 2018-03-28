using System;
using System.Collections.Generic;
using Contracts.Logic;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContractsTest
{
    [TestClass]
    public class BeContractSerializeTest
    {
        #region Initializing
        public Validators Validators { get; set; }

        public BeContractSerializeTest()
        {
            //Validators = new Validators();
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
                /*Queries = new List<BeContract>(){
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
                }*/
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

        public string GetContractString()
        {
            return @"
            {
                'Id': 'GetDogOwner',
                'Description': 'This contract is used to get the dog owner',
                'Version': 'V001',
                'Inputs': [
                {
		            'Type': 'String',
		            'Key': 'DogID',
		            'Required': true,
		            'Description': 'The ID of the Dog'
                },
                {
		            'Type': 'Int32',
		            'Key': 'Age',
		            'Required': false,
		            'Description': 'Age of the dog'
                }],
                'Query': [
                    {
                        'Id': 'GetCitizenIDfromDogID',
                        'Description': 'This contract is used to get the dog owner ID',
                        'Version': 'V001',
                        'Inputs': [
                            {
		                        'Type': 'String',
		                        'Key': 'DogID',
		                        'Required': true,
		                        'Description': 'The ID of the Dog'
                            }
                        ],
                        'Query': null,
                        'Outputs':null
                    }
                ],
                'Outputs': [
                {
                    'Contract': 'GetDogOwner',
                    'Type': 'String',
                    'Description': 'The ID of the owner of the dog',
                    'Key': 'OwnerID'
                }
                ]
            }";
        }

        #endregion

        [TestMethod]
        public void TestSerializeBeContract()
        {
            var json = new Generators().SerializeBeContract(CreateGoodContract());
            Assert.AreEqual(GetContractString().Replace("\"", "'").Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", ""), 
                json.Replace(" ", "").Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("\t", ""));
        }

        [TestMethod]
        public void TestDeserializeBeContract()
        {
            var contractByJson = new Generators().DeserializeBeContract(GetContractString());
            var contract = CreateGoodContract();
            Assert.AreEqual(contract.Description, contractByJson.Description);
            Assert.AreEqual(contract.Id, contractByJson.Id);
            Assert.AreEqual(contract.Version, contractByJson.Version);
            for(int i = 0; i < contract.Inputs.Count; i++)
            {
                Assert.AreEqual(contract.Inputs[i].Description, contractByJson.Inputs[i].Description);
                Assert.AreEqual(contract.Inputs[i].Key, contractByJson.Inputs[i].Key);
                Assert.AreEqual(contract.Inputs[i].Required, contractByJson.Inputs[i].Required);
                Assert.AreEqual(contract.Inputs[i].Type, contractByJson.Inputs[i].Type);
            }
            for (int i = 0; i < contract.Outputs.Count; i++)
            {
                Assert.AreEqual(contract.Outputs[i].Description, contractByJson.Outputs[i].Description);
                Assert.AreEqual(contract.Outputs[i].Key, contractByJson.Outputs[i].Key);
                Assert.AreEqual(contract.Outputs[i].Contract.Id, contractByJson.Outputs[i].Contract.Id);
                Assert.AreEqual(contract.Outputs[i].Type, contractByJson.Outputs[i].Type);
            }
        }
    }
}
