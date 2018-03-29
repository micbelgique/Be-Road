using Contracts.Dal;
using Contracts.Dal.Mock;
using Contracts.Logic;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting
{
    class Program
    {


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
            };

            GetDogOwnerContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    Contract = GetDogOwnerContract,
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
            };

            GetAddressByOwnerContract.Outputs = new List<Output>()
            {
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "Street",
                    Description = "Street name",
                    Type = typeof(string).Name
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "StreetNumber",
                    Description = "Street number",
                    Type = typeof(int).Name
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "Country",
                    Description = "Country of the address",
                    Type = typeof(string).Name
                }
            };
            return GetAddressByOwnerContract;
        }

        public static BeContract GetAddressByDogId(ContractContext db)
        {
            var GetAddressByOwnerContract = db.Contracts.FirstOrDefault(c => c.Id.Equals("GetAddressByOwnerId"));
            var GetOwnerIdByDogContract = db.Contracts.FirstOrDefault(c => c.Id.Equals("GetOwnerIdByDogId"));

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
                    Type = typeof(string).Name
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
                    Key = "StreetNumber",
                    Description = "Street number",
                    Type = typeof(int).Name
                },
                new Output()
                {
                    Contract = GetAddressByOwnerContract,
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
                        Description = "Second number",
                        Required = false,
                        Type = typeof(int).Name
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
                    Type = typeof(int).Name
                },
                new Output()
                {
                    Contract = GetMathemathicContract,
                    Key = "Formula",
                    Description = "This is the sum formula",
                    Type = typeof(string).Name
                }
            };
            return GetMathemathicContract;
        }

        static BeContractCall CreateContractCall(string id, params string[] parameters)
        {
            var call = new BeContractCall()
            {
                Id = id,
                Inputs = new Dictionary<string, dynamic>()
            };
            parameters.ToList().ForEach(param =>
            {
                var split = param.Split(':');
                if(split.Length == 2)
                {
                    if(int.TryParse(split[1], out int n))
                        call.Inputs.Add(split[0], n);
                    else
                        call.Inputs.Add(split[0], split[1]);
                }
            });
            return call;
        }

        static void AddOrUpdate(ContractContext db, BeContract contract)
        {
            if (!db.Contracts.Any(c => c.Id.Equals(contract.Id)))
            {
                Console.WriteLine($"Saving {contract.Id}");
                db.Contracts.Add(contract);
                db.SaveChanges();
            }
        }

        public static void TestDeserializedContracts(BeContract expected, BeContract actual)
        {
            Console.WriteLine(expected.Description == actual.Description);
            Console.WriteLine(expected.Id == actual.Id);
            Console.WriteLine(expected.Version == actual.Version);
            for (int i = 0; i < expected.Inputs.Count; i++)
            {
                Console.WriteLine(expected.Inputs[i].Description == actual.Inputs[i].Description);
                Console.WriteLine(expected.Inputs[i].Key == actual.Inputs[i].Key);
                Console.WriteLine(expected.Inputs[i].Required == actual.Inputs[i].Required);
                Console.WriteLine(expected.Inputs[i].Type == actual.Inputs[i].Type);
            }
            for (int i = 0; i < expected.Queries.Count; i++)
            {
                Console.WriteLine(expected.Queries[i].Contract.Id == actual.Queries[i].Contract.Id);
                for (int j = 0; j < expected.Queries[i].Mappings.Count; j++)
                {
                    Console.WriteLine(expected.Queries[i].Mappings[j].ContractKey == actual.Queries[i].Mappings[j].ContractKey);
                    Console.WriteLine(expected.Queries[i].Mappings[j].Contract.Id == actual.Queries[i].Mappings[j].Contract.Id);
                    Console.WriteLine(expected.Queries[i].Mappings[j].InputKey == actual.Queries[i].Mappings[j].InputKey);
                }
            }

            for (int i = 0; i < expected.Outputs.Count; i++)
            {
                Console.WriteLine(expected.Outputs[i].Description == actual.Outputs[i].Description);
                Console.WriteLine(expected.Outputs[i].Key == actual.Outputs[i].Key);
                Console.WriteLine(expected.Outputs[i].Contract.Id == actual.Outputs[i].Contract.Id);
                Console.WriteLine(expected.Outputs[i].Type == actual.Outputs[i].Type);
            }
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            var manager = new ContractManager();
            var db = new ContractContext();

            var owner = db.Contracts.First(c => c.Id.Equals("GetAddressByDogId"));
            TestDeserializedContracts(owner, GetAddressByDogId(db));

           /* db.Contracts.RemoveRange(db.Contracts);
            db.SaveChanges();
            Console.WriteLine(db.Contracts.Count());

            AddOrUpdate(db, GetOwnerIdByDogId());
            AddOrUpdate(db, GetAddressByOwnerId());
            AddOrUpdate(db, GetAddressByDogId(db));
            AddOrUpdate(db, GetMathemathicFunction());

            Console.WriteLine(db.Contracts.Count());*/

            /*manager.Call(CreateContractCall("GetOwnerIdByDogId", "DogID:D-123"));
            manager.Call(CreateContractCall("GetOwnerIdByDogId", "DogID:D-122"));
            manager.Call(CreateContractCall("GetOwnerIdByDogId", "DogID:"));
            manager.Call(CreateContractCall("GetAddressByOwnerId", "OwnerID:Mika !"));
            manager.Call(CreateContractCall("GetAddressByDogId", "MyDogID:D-123"));
            manager.Call(CreateContractCall("GetMathemathicFunction", "A:5", "B:19"));*/
            Console.ReadLine();
        }
    }
}
