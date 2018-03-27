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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract">
        /// The contract is temporary for testing purpose, 
        /// later we will get the contract with the contractId that's inside the call
        /// </param>
        /// <param name="call"></param>
        public void CallTest(BeContract contract, BeContractCall call)
        {
            //TODO: 
            //  -Get the contract from the DB instead of passing by arguments
            //  -Check if the contract exist
            //  -Validate the contract ?
            //  -Check if the caller has the permission to use the contract
            Validators.ValidateBeContractCall(contract, call);

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

            Console.WriteLine($"Calling contract {contract.Id}");
            contract.Inputs.ForEach(input =>
            {
                if (call.Inputs.TryGetValue(input.Key, out dynamic value))
                {
                    Console.WriteLine($"{value} was given as argument for {input.Key}");   
                }
            });
            Console.WriteLine("End calling contract");
        }
    }
}
