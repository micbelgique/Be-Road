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

        public BeContract CreateGoodContract()
        {
            return BeContractsMock.GetOwnerIdByDogId();
        }

        public string GetContractString()
        {
            return @"
                {
                  'Id': 'GetOwnerIdByDogId',
                  'Description': 'This contract is used to get the dog owner id',
                  'Version': 'V001',
                  'Inputs': [
                    {
                      'Type': 'String',
                      'Key': 'DogID',
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

        #endregion

        [TestMethod]
        public void TestSerializeBeContract()
        {
            var json = new Generators().SerializeBeContract(CreateGoodContract());
            var expected = Regex.Replace(GetContractString(), @"\s+", "").Replace("'", "\"");
            var actual = Regex.Replace(json, @"\s+", "");
            Assert.AreEqual(expected, actual);
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
