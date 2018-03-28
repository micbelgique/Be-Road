using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal.Mock
{
    public class BeContractsMock
    {
        public static List<BeContract> GetContracts()
        {
            return new List<BeContract>()
            {
                GetOwnerIdByDogId(),
                GetMathemathicFunction(),
                GetAddressByOwnerId(),
                GetAddressByDogId()
            };
        }

        public static BeContract GetOwnerIdByDogId()
        {
            var GetDogOwnerContract = new BeContract()
            {
                Id = "GetOwnerIdByDogId",
                Description = "This contract is used to get the dog owner id",
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
            };

            GetDogOwnerContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    Contract = GetDogOwnerContract,
                    Key = "OwnerIDOfTheDog",
                    Description = "The ID of the owner of the dog",
                    Type = typeof(string)
                }
            };
            return GetDogOwnerContract;
        }

        public static BeContract GetAddressByOwnerId()
        {
            var GetAddressByOwnerContract = new BeContract()
            {
                Id = "GetAddressByOwnerId",
                Description = "This contract is used to get the address by owner id",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "OwnerID",
                        Description = "The ID of the Owner",
                        Required = true,
                        Type = typeof(string)
                    }
                },
            };

            GetAddressByOwnerContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "Street",
                    Description = "Street name",
                    Type = typeof(string)
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "StreetNumber",
                    Description = "Street number",
                    Type = typeof(int)
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "Country",
                    Description = "Country of the address",
                    Type = typeof(string)
                }
            };
            return GetAddressByOwnerContract;
        }

        public static BeContract GetAddressByDogId()
        {
            var GetAddressByOwnerContract = GetAddressByOwnerId();
            var GetOwnerIdByDogContract = GetOwnerIdByDogId();

            var GetAddressByDogContract = new BeContract()
            {
                Id = "GetAddressByDogId",
                Description = "This contract is used to get the address by dog id",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "MyDogID",
                        Description = "The ID of the Dog",
                        Required = true,
                        Type = typeof(string)
                    }
                },
            };

            GetAddressByDogContract.Queries = new List<Query>()
            {
                new Query()
                {
                    Contract = GetOwnerIdByDogContract,
                    Mappings = new List<Mapping>()
                    {
                        new Mapping()
                        {
                            InputKey = "DogID",
                            Contract = GetAddressByDogContract,
                            ContractKey = "MyDogID"
                        }
                    }
                },
                new Query()
                {
                    Contract = GetAddressByOwnerContract,
                    Mappings = new List<Mapping>()
                    {
                        new Mapping()
                        {
                            InputKey = "OwnerID",
                            Contract = GetOwnerIdByDogContract,
                            ContractKey = "OwnerIDOfTheDog"
                        }
                    }
                }
            };

            GetAddressByDogContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "Street",
                    Description = "Street name",
                    Type = typeof(string)
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "StreetNumber",
                    Description = "Street number",
                    Type = typeof(int)
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "Country",
                    Description = "Country of the address",
                    Type = typeof(string)
                }
            };

            return GetAddressByDogContract;
        }

        public static BeContract GetMathemathicFunction()
        {
            var GetMathemathicContract = new BeContract()
            {
                Id = "GetMathemathicFunction",
                Description = "This contract is used to do a + b",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "A",
                        Description = "First number",
                        Required = true,
                        Type = typeof(int)
                    },
                    new Input()
                    {
                        Key = "B",
                        Description = "First number",
                        Required = true,
                        Type = typeof(int)
                    }
                },
            };

            GetMathemathicContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    Contract = GetMathemathicContract,
                    Key = "Total",
                    Description = "This is the sum of a + b",
                    Type = typeof(int)
                },
                new Output()
                {
                    Contract = GetMathemathicContract,
                    Key = "Formula",
                    Description = "This is the sum formula",
                    Type = typeof(string)
                }
            };
            return GetMathemathicContract;
        }
    }
}
