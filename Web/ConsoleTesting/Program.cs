using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
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

            //var ctx = new ContractContext();
            //ctx.Contracts.Add(BeContractsMock.GetAddressByDogId());
            //ctx.Contracts.Add(BeContractsMock.GetAddressByOwnerId());
            //ctx.Contracts.Add(BeContractsMock.GetOwnerIdByDogId());
            //ctx.Contracts.Add(BeContractsMock.GetMathemathicFunction());
            //ctx.SaveChanges();
            //Console.WriteLine(ctx.Contracts.Count());
            var manager = new ContractManager()
            {
                AsService = new AdapterServerServiceImpl(),
                BcService = new BeContractService()
            };

            manager.CallAsync(CreateContractCall("GetOwnerIdByDogId", "DogID:D-123")).Wait();
            manager.CallAsync(CreateContractCall("GetOwnerIdByDogId", "DogID:D-122")).Wait();
            manager.CallAsync(CreateContractCall("GetOwnerIdByDogId", "DogID:")).Wait();
            manager.CallAsync(CreateContractCall("GetAddressByOwnerId", "OwnerID:Mika !")).Wait();
            manager.CallAsync(CreateContractCall("GetAddressByDogId", "MyDogID:D-123")).Wait();
            manager.CallAsync(CreateContractCall("GetMathemathicFunction", "A:5", "B:19")).Wait();
            Console.ReadLine();
        }
    }
}
