using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.Migrations
{
    public class PrivacyPassportContracts
    {
        public static BeContract GetPopulationContract()
        {
            var contract = new BeContract()
            {
                Id = "GetPopulationContract",
                Description = "This contract is used to get the basics information about a citizen",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "NRID",
                        Description = "The ID of the citizen",
                        Required = true,
                        Type = typeof(string).Name
                    },
                    new Input()
                    {
                        Key = "Justification",
                        Description = "Why are you reading this information",
                        Required = true,
                        Type = typeof(string).Name
                    }
                },
                Queries = new List<Query>()
            };

            contract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "FirstName",
                    Description = "First name of the citizen",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "LastName",
                    Description = "Last name of the citizen",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Birthday",
                    Description = "Birthday of the citizen",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Locality",
                    Description = "Locality of the citizen",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Nationality",
                    Description = "Nationality of the citizen",
                    Type = typeof(string).Name
                },
            };
            return contract;
        }

        public static BeContract GetDIVContract()
        {
            var contract = new BeContract()
            {
                Id = "GetDivContract",
                Description = "This contract is used to get information about a citizen car's",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "NRID",
                        Description = "The ID of the citizen",
                        Required = true,
                        Type = typeof(string).Name
                    },
                    new Input()
                    {
                        Key = "Justification",
                        Description = "Why are you reading this information",
                        Required = true,
                        Type = typeof(string).Name
                    }
                },
                Queries = new List<Query>()
            };

            contract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "NumberPlate",
                    Description = "Number plate of the citizen",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Brand",
                    Description = "Brand of the car",
                    Type = typeof(string).Name
                }
            };
            return contract;
        }

        public static BeContract GetBankContract()
        {
            var contract = new BeContract()
            {
                Id = "GetBankContract",
                Description = "This contract is used to get information about a citizen bank account",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "NRID",
                        Description = "The ID of the citizen",
                        Required = true,
                        Type = typeof(string).Name
                    },
                    new Input()
                    {
                        Key = "Justification",
                        Description = "Why are you reading this information",
                        Required = true,
                        Type = typeof(string).Name
                    }
                },
                Queries = new List<Query>()
            };

            contract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Bankaccount",
                    Description = "Bankaccount of the citizen",
                    Type = typeof(string).Name
                }
            };
            return contract;
        }

        public static BeContract GetFunnyContract()
        {
            var contract = new BeContract()
            {
                Id = "GetFunnyContract",
                Description = "This contract is used to get some funny information about a citizen",
                Version = "V001",
                Inputs = new List<Input>()
                {
                    new Input()
                    {
                        Key = "NRID",
                        Description = "The ID of the citizen",
                        Required = true,
                        Type = typeof(string).Name
                    },
                    new Input()
                    {
                        Key = "Justification",
                        Description = "Why are you reading this information",
                        Required = true,
                        Type = typeof(string).Name
                    }
                },
                Queries = new List<Query>()
            };

            contract.Outputs = new List<Output>()
            {
                new Output()
                {
                    LookupInputId = 0,
                    Key = "ExtraInfo",
                    Description = "The info that a citizen share's",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    LookupInputId = 0,
                    Key = "Email",
                    Description = "Email of a citizen share's",
                    Type = typeof(string).Name
                }
            };
            return contract;
        }
    }
}