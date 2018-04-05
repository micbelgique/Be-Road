using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Proxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTesting
{
    class Program
    {
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

        static void Main(string[] args)
        {
            //Used for connection string
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            var asService = new AdapterServerService();
            asService.SetADSList(ASSMock.Fill());

            var ctx = new ContractContext();
            ctx.Contracts.AddRange(Contracts.Dal.Mock.BeContractsMock.GetContracts(ctx));
            ctx.SaveChanges();
            Console.WriteLine(ctx.Contracts.Count());
            var manager = new ContractManager(asService);

            manager.Call(CreateContractCall("GetOwnerIdByDogId", "DogID:D-123"));
            manager.Call(CreateContractCall("GetOwnerIdByDogId", "DogID:D-122"));
            manager.Call(CreateContractCall("GetOwnerIdByDogId", "DogID:"));
            manager.Call(CreateContractCall("GetAddressByOwnerId", "OwnerID:Mika !"));
            manager.Call(CreateContractCall("GetAddressByDogId", "MyDogID:D-123"));
            manager.Call(CreateContractCall("GetMathemathicFunction", "A:5", "B:19"));
            Console.ReadLine();
        }
    }
}
