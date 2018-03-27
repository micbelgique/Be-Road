using Contracts.Logic;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting
{
    class Program
    {
        public BeContractCall GetBeContractCallString()
        {
            return new BeContractCall()
            {
                Id = "GetDogOwner",
                Inputs = new Dictionary<string, dynamic>()
                {
                    { "DogID", "EG-673KL" },
                    { "Age", 54}
                }
            };
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

        static void Main(string[] args)
        {
            var manager = new ContractManager();
            var program = new Program();
            manager.CallTest(program.CreateGoodContract(), program.GetBeContractCallString());
            Console.Read();
        }
    }
}
