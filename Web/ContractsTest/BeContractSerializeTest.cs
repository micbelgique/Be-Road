using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            Validators = new Validators();
        }

        public string GetOwnerIdByDogIdString()
        {
            return @"
                {
                  'Id': 'GetOwnerIdByDogId',
                  'Description': 'This contract is used to get the dog owner id',
                  'Version': 'V001',
                  'Inputs': [
                    {
                      'Key': 'DogID',
                      'Type': 'String',
                      'Required': true,
                      'Description': 'The ID of the Dog'
                    }
                  ],
                  'Queries': [],
                  'Outputs': [
                    {
                      'Contract': 'GetOwnerIdByDogId',
                      'Type': 'String',
                      'Description': 'The ID of the owner of the dog',
                      'Key': 'OwnerIDOfTheDog'
                    }
                  ]
                }";
        }

        public string GetAddressByDogIdString()
        {
            return @"
                {
                  'Id': 'GetAddressByDogId',
                  'Description': 'This contract is used to get the address by dog id',
                  'Version': 'V001',
                  'Inputs': [
                    {
                      'Key': 'MyDogID',
                      'Type': 'String',
                      'Required': true,
                      'Description': 'The ID of the Dog'
                    }
                  ],
                  'Queries': [
                    {
                      'Contract': 'GetOwnerIdByDogId',
                      'Mappings': [
                        {
                          'InputKey': 'DogID',
                          'Contract': 'GetAddressByDogId',
                          'ContractKey': 'MyDogID'
                        }
                      ]
                    },
                    {
                      'Contract': 'GetAddressByOwnerId',
                      'Mappings': [
                        {
                          'InputKey': 'OwnerID',
                          'Contract': 'GetOwnerIdByDogId',
                          'ContractKey': 'OwnerIDOfTheDog'
                        }
                      ]
                    }
                  ],
                  'Outputs': [
                    {
                      'Contract': 'GetAddressByOwnerId',
                      'Type': 'String',
                      'Description': 'Street name',
                      'Key': 'Street'
                    },
                    {
                      'Contract': 'GetAddressByOwnerId',
                      'Type': 'Int32',
                      'Description': 'Street number',
                      'Key': 'StreetNumber'
                    },
                    {
                      'Contract': 'GetAddressByOwnerId',
                      'Type': 'String',
                      'Description': 'Country of the address',
                      'Key': 'Country'
                    }
                  ]
                }";
        }

        #endregion

        public void TestSerializedJson(string expected, string actual)
        {
            expected = Regex.Replace(expected, @"\s+", "").Replace("'", "\"");
            actual = Regex.Replace(actual, @"\s+", "");
            Assert.AreEqual(expected, actual);
        }

        public void TestDeserializedContracts(BeContract expected, BeContract actual)
        {
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Version, actual.Version);
            for (int i = 0; i < expected.Inputs.Count; i++)
            {
                Assert.AreEqual(expected.Inputs[i].Description, actual.Inputs[i].Description);
                Assert.AreEqual(expected.Inputs[i].Key, actual.Inputs[i].Key);
                Assert.AreEqual(expected.Inputs[i].Required, actual.Inputs[i].Required);
                Assert.AreEqual(expected.Inputs[i].Type, actual.Inputs[i].Type);
            }
            for (int i = 0; i < expected.Outputs.Count; i++)
            {
                Assert.AreEqual(expected.Outputs[i].Description, actual.Outputs[i].Description);
                Assert.AreEqual(expected.Outputs[i].Key, actual.Outputs[i].Key);
                Assert.AreEqual(expected.Outputs[i].Contract.Id, actual.Outputs[i].Contract.Id);
                Assert.AreEqual(expected.Outputs[i].Type, actual.Outputs[i].Type);
            }
        }

        [TestMethod]
        public void TestSerializeBeContractWithoutQuery()
        {
            var json = Validators.Generators.SerializeBeContract(BeContractsMock.GetOwnerIdByDogId());
            TestSerializedJson(GetOwnerIdByDogIdString(), json);
        }


        [TestMethod]
        public void TestDeserializeBeContractWithoutQuery()
        {
            var contractByJson = Validators.Generators.DeserializeBeContract(GetOwnerIdByDogIdString());
            var contract = BeContractsMock.GetOwnerIdByDogId();
            TestDeserializedContracts(contract, contractByJson);
        }

        [TestMethod]
        public void TestSerializeBeContractWithQuery()
        {
            var json = Validators.Generators.SerializeBeContract(BeContractsMock.GetAddressByDogId());
            TestSerializedJson(GetAddressByDogIdString(), json);
        }

        [TestMethod]
        public void TestDeserializeBeContractWithQuery()
        {
            var contractByJson = Validators.Generators.DeserializeBeContract(GetAddressByDogIdString());
            var contract = BeContractsMock.GetAddressByDogId();
            TestDeserializedContracts(contract, contractByJson);
        }
    }
}
