using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Dal.Mock
{
    public class BeContractsMock
    {
        public static List<BeContract> GetContracts()
        {
            return new List<BeContract>()
            {
                GetServiceInfo(),
                GetOwnerIdByDogId(),
                GetMathemathicFunction(),
                GetAddressByOwnerId(),
                GetAddressByDogId()
            };
        }

        public static BeContract GetServiceInfo()
        {
            var GetServiceInfoContract =  new BeContract()
            {
                Id = "GetServiceInfo",
                Description = "This contract is used to get the information of the service",
                Version = "V001",
                Inputs = new List<Input>(),
                Queries = new List<Query>()                
            };

            GetServiceInfoContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Name",
                    Description = "The name of the service",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Purpose",
                    Description = "The purpose of the service",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "CreationDate",
                    Description = "The creation date of the service",
                    Type = typeof(string).Name
                }
            };

            return GetServiceInfoContract;
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
                        Type = typeof(string).Name
                    }
                },
                Queries = new List<Query>()
            };

            GetDogOwnerContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "OwnerIDOfTheDog",
                    Description = "The ID of the owner of the dog",
                    Type = typeof(string).Name
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
                        Type = typeof(string).Name
                    }
                },
                Queries = new List<Query>()
            };

            GetAddressByOwnerContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Street",
                    Description = "Street name",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "StreetNumber",
                    Description = "Street number",
                    Type = typeof(int).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Country",
                    Description = "Country of the address",
                    Type = typeof(string).Name
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
                        Type = typeof(string).Name
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
                            LookupInputId = 0,
                            LookupInputKey = "MyDogID"
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
                            LookupInputId = 1,
                            LookupInputKey = "OwnerIDOfTheDog"
                        }
                    }
                }
            };

            GetAddressByDogContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 1,
                    Key = "Street",
                    Description = "Street name",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 1,
                    Key = "StreetNumber",
                    Description = "Street number",
                    Type = typeof(int).Name
                },
                new Output()
                {
                    LookupInputId = 1,
                    Key = "Country",
                    Description = "Country of the address",
                    Type = typeof(string).Name
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
                        Type = typeof(int).Name
                    },
                    new Input()
                    {
                        Key = "B",
                        Description = "First number",
                        Required = true,
                        Type = typeof(int).Name
                    }
                },
            };

            GetMathemathicContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Total",
                    Description = "This is the sum of a + b",
                    Type = typeof(int).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Formula",
                    Description = "This is the sum formula",
                    Type = typeof(string).Name
                }
            };
            return GetMathemathicContract;
        }
        public static BeContract GetDoubleInputContract()
        {
            var DoubleInputContract = new BeContract()
            {
                Id = "DoubleInputContract",
                Description = "This contract is an examples with 2 inputs",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "First",
                        Description = "First string",
                        Required = false,
                        Type = typeof(int).Name
                    },
                    new Input()
                    {
                        Key = "Second",
                        Description = "First number",
                        Required = false,
                        Type = typeof(int).Name
                    }
                },
            };

            DoubleInputContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "IsFirstNull",
                    Description = "Return true if the first input is empty",
                    Type = typeof(bool).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "IsSecondNull",
                    Description = "Return true if the first input is empty",
                    Type = typeof(bool).Name
                }
            };
            return DoubleInputContract;
        }
    }
}
