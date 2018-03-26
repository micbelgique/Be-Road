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
    }
}
