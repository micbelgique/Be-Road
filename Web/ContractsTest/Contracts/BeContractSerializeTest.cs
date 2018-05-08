using Contracts.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace BeRoadTest.Contracts
{
    [TestClass]
    public class BeContractSerializeTest
    {
        #region Initializing
        public Validators Validators { get; set; }
        public BeContractEqualsTest BeContractEquals { get; set; }

        [TestInitialize]
        public void Init()
        {
            Validators = new Validators();
            BeContractEquals = new BeContractEqualsTest();
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
                      'LookupInputId': 0,
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
                          'LookupInputId': 0,
                          'LookupInputKey': 'MyDogID'
                        }
                      ]
                    },
                    {
                      'Contract': 'GetAddressByOwnerId',
                      'Mappings': [
                        {
                          'InputKey': 'OwnerID',
                          'LookupInputId': 1,
                          'LookupInputKey': 'OwnerIDOfTheDog'
                        }
                      ]
                    }
                  ],
                  'Outputs': [
                    {
                      'LookupInputId': 1,
                      'Type': 'String',
                      'Description': 'Street name',
                      'Key': 'Street'
                    },
                    {
                      'LookupInputId': 1,
                      'Type': 'Int32',
                      'Description': 'Street number',
                      'Key': 'StreetNumber'
                    },
                    {
                      'LookupInputId': 1,
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
            BeContractEquals.AreEquals(contract, contractByJson);
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
            BeContractEquals.AreEquals(contract, contractByJson);
        }
    }
}
