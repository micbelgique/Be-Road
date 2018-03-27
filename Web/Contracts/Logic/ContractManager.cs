using Contracts.Dal;
using Contracts.Dal.Mock;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Logic
{
    public class ContractManager
    {
        private Validators Validators { get; set; }

        public ContractManager()
        {
            Validators = new Validators();
        }
        
        public void Call(BeContract contract, BeContractCall call)
        {
            //TODO: 
            //  -Check if the contract exist
            //  -Validate the contract ?
            //  -Check if the caller has the permission to use the contract
            if (contract == null)
                throw new BeContractException("Contract is null");
            if (call == null)
                throw new BeContractException("Contract call is null");

            Validators.ValidateBeContractCall(contract, call);
            Console.WriteLine($"Calling contract {contract.Id}");
            
            if (call.Id.Equals("GetOwnerIdByDogId"))
                VeterinaryMock.GetOwnerId(call);
            else if (call.Id.Equals("GetMathemathicFunction"))
                MathematicsMock.GetSumFunction(call);
            else if (call.Id.Equals("GetAddressByOwnerId"))
                AddressMock.GetAddressByOwnerId(call);
            else if (call.Id.Equals("GetAddressByDogId"))
                Console.WriteLine("Queries not yet supported");
            else 
                Console.WriteLine($"No service found for {call.Id}");

            /* contract.Query.ForEach(q => 
             {
                 var callInQuery = new BeContractCall()
                 {
                     Id = q.Id,
                     Inputs = new Dictionary<string, dynamic>()
                 };

                 contract.Inputs.ForEach(input =>
                 {
                     if (call.Inputs.TryGetValue(input.Key, out dynamic value))
                     {
                         callInQuery.Inputs.Add(input.Key, value);
                     }
                 });
             });*/
        }

        public void Call(BeContractCall call)
        {
            var contract = BeContractsMock.GetContracts().FirstOrDefault(c => c.Id == call.Id);
            Call(contract, call);
        }
    }
}
