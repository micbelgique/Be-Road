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
        
        /// <summary>
        /// Call the contract with the specified inputs to a specified service
        /// </summary>
        /// <param name="contract">Contract that's will be called</param>
        /// <param name="call">The inputs for the contract</param>
        /// <returns>Returns the outputs for that specific contract</returns>
        private BeContractReturn CallService(BeContract contract, BeContractCall call)
        {
            //Forward the call to the good service
            BeContractReturn returns = null;
            if (call.Id.Equals("GetOwnerIdByDogId"))
                returns = VeterinaryMock.GetOwnerId(call);
            else if (call.Id.Equals("GetMathemathicFunction"))
                returns = MathematicsMock.GetSumFunction(call);
            else if (call.Id.Equals("GetAddressByOwnerId"))
                returns = AddressMock.GetAddressByOwnerId(call);
            else if (call.Id.Equals("GetAddressByDogId"))
                Console.WriteLine("Queries not yet supported");
            else
                throw new BeContractException($"No service found for {call.Id}") { BeContractCall = call };

            //TODO:
            //  -Send error to the service
            if (returns != null)
                Validators.ValidateBeContractReturn(contract, returns);

            return returns;
        }

        /// <summary>
        /// Find the contract with the call id 
        /// Loop over every query
        /// </summary>
        /// <param name="call">The inputs for the contract call</param>
        /// <returns>Return a list of contract id with his return types</returns>
        private List<BeContractReturn> CallAndLoopQueries(BeContractCall call, BeContract contract = null)
        {
            if(contract == null)
                contract = BeContractsMock.GetContracts().FirstOrDefault(c => c.Id == call.Id);

            //TODO: 
            //  -Check if the caller has the permission to use the contract
            if (contract == null)
                throw new BeContractException("Contract is null");
            if (call == null)
                throw new BeContractException("Contract call is null");

            Validators.ValidateBeContractCall(contract, call);

            var returns = new List<BeContractReturn>();

            //If it's a nested contract, loop through it
            if (contract?.Queries?.Count > 0)
            {
                contract.Queries.ForEach(q =>
                {
                    //Create the BeContractCall
                    var callInQuery = new BeContractCall()
                    {
                        Id = q.Contract.Id,
                        Inputs = new Dictionary<string, dynamic>()
                    };

                    //Fill in the inputs of the BeContractCall
                    q.Contract.Inputs.ForEach(input =>
                    {
                        var mapping = q.Mappings.FirstOrDefault(m => m.InputKey.Equals(input.Key));
                        if(contract.Id.Equals(mapping.Contract.Id))
                        {
                            //We need to check our contract input
                            if (!call.Inputs.TryGetValue(mapping.ContractKey, out dynamic value))
                                throw new BeContractException($"No value was found for the key {mapping.ContractKey} in the contract input of {mapping.Contract.Id}")
                                { BeContractCall = call };

                            callInQuery.Inputs.Add(input.Key, value);
                        }
                        else
                        {
                            //Find the contract where we need to search the value
                            var returnToBeUsed = returns.FirstOrDefault(ret => ret.Id.Equals(mapping.Contract.Id));
                            if (returnToBeUsed == null)
                                throw new BeContractException($"No output was found for query {q.Contract.Id} with mapping contract {mapping.Contract.Id}")
                                { BeContractCall = call };

                            //We need to check our contract outputs
                            if (!returnToBeUsed.Outputs.TryGetValue(mapping.ContractKey, out dynamic value))
                                throw new BeContractException($"No value was found for the key {mapping.ContractKey} in the contract outputs of {mapping.Contract.Id}")
                                { BeContractCall = call };

                            callInQuery.Inputs.Add(input.Key, value);
                        }
                    });

                    //Call the contract
                    returns.AddRange(CallAndLoopQueries(callInQuery));
                });
            }
            else
            {
                returns.Add(CallService(contract, call));
            }
          
            return returns;
        }

        /// <summary>
        /// Call a contract
        /// </summary>
        /// <param name="call">The inputs for the contract call</param>
        /// <returns></returns>
        public BeContractReturn Call(BeContractCall call)
        {
            var contract = BeContractsMock.GetContracts().FirstOrDefault(c => c.Id == call.Id);
            Console.WriteLine($"Calling contract {contract.Id}");
            //Filter to onlt gives the correct outputs
            var filtredReturns = CallAndLoopQueries(call, contract)
                .SelectMany(r => r.Outputs)
                .Where(pair => contract.Outputs.Any(output => output.Key.Equals(pair.Key)))
                .ToDictionary(pair => pair.Key, pair => pair.Value)
                ;
            var contractReturn = new BeContractReturn()
            {
                Id = contract.Id,
                Outputs = filtredReturns
            };
            
            contractReturn.Outputs.ToList().ForEach(output =>
            {
                Console.WriteLine($"-{output.Key} = {output.Value}");
            });

            return contractReturn;
        }
    }
}
