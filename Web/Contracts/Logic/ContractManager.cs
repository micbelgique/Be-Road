using Contracts.Dal;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Logic
{
    public class ContractManager
    {
        private Validators validators;

        public IAdapterServerService AsService { get; set; }
        public IBeContractService BcService { get; set; }


        public ContractManager()
        {
            validators = new Validators();
        }
        
        /// <summary>
        /// Call the contract with the specified inputs to a specified service
        /// </summary>
        /// <param name="contract">Contract that will be called</param>
        /// <param name="call">The inputs for the contract</param>
        /// <returns>Returns the outputs for that specific contract</returns>
        private async Task<BeContractReturn> CallServiceAsync(BeContract contract, BeContractCall call)
        {
            //Forward the call to the good service
            BeContractReturn returns = await AsService.CallAsync(call);

            //TODO:
            //  -Send error to the service
            if (returns != null)
                validators.ValidateBeContractReturn(contract, returns);

            return returns;
        }

        /// <summary>
        /// Find the contract with the call id 
        /// Loop over every query
        /// </summary>
        /// <param name="call">The inputs for the contract call</param>
        /// <returns>Return a list of contract id with his return types</returns>
        private async Task<List<BeContractReturn>> CallAndLoopQueriesAsync(BeContractCall call, BeContract contract = null)
        {
            if (contract == null)
                contract = BcService.FindBeContractById(call.Id);

            if (contract == null)
                throw new BeContractException($"No contract was found with id {call.Id}");

            validators.ValidateBeContractCall(contract, call);

            var returns = new List<BeContractReturn>();

            //If it's a nested contract, loop through it
            if (contract?.Queries?.Count > 0)
            {
                foreach(var q in contract.Queries)
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
                        if (mapping == null)
                            throw new BeContractException($"No mapping exists for inputKey {input.Key}");

                        //We need to check our contract input
                        if (mapping.LookupInputId == 0)
                        {
                            if (!call.Inputs.TryGetValue(mapping.LookupInputKey, out dynamic value))
                                throw new BeContractException($"No value was found for the key {mapping.LookupInputKey} in the lookupinputid n°{mapping.LookupInputId}")
                                { BeContractCall = call };

                            callInQuery.Inputs.Add(input.Key, value);
                        }
                        //Find the contract where we need to search the value
                        else
                        {
                            if ((mapping.LookupInputId - 1) >= returns.Count)
                                throw new BeContractException($"Mapping LookupInputId n°{mapping.LookupInputId} is bigger then the returns size {returns.Count}");
                            var returnToBeUsed = returns[mapping.LookupInputId - 1];

                            if (returnToBeUsed == null)
                                throw new BeContractException($"No output was found for query {q.Contract.Id} with mapping lookupinputid n°{mapping.LookupInputId}")
                                { BeContractCall = call };

                            //We need to check our contract outputs
                            if (!returnToBeUsed.Outputs.TryGetValue(mapping.LookupInputKey, out dynamic value))
                                throw new BeContractException($"No value was found for the key {mapping.LookupInputKey} in the contract outputs of n°{mapping.LookupInputId}")
                                { BeContractCall = call };

                            callInQuery.Inputs.Add(input.Key, value);
                        }
                    });

                    //Call the contract
                    var loopdQueries = await CallAndLoopQueriesAsync(callInQuery);
                    if (loopdQueries.Contains(null))
                        throw new BeContractException($"Got an null in loopqueries with id {callInQuery.Id}");
                    returns.AddRange(loopdQueries);
                };
            }
            else
            {
                var serviceReturns = await CallServiceAsync(contract, call);
                if (serviceReturns == null)
                    throw new BeContractException($"Got an null in serviceCall with id {call.Id}");
                returns.Add(serviceReturns);
            }
          
            return returns;
        }

        /// <summary>
        /// Call a contract
        /// </summary>
        /// <param name="call">The inputs for the contract call</param>
        /// <returns></returns>
        public async Task<Dictionary<int, BeContractReturn>> CallAsync(BeContractCall call)
        {
            if (call == null)
                throw new BeContractException("Contract call is null");

            var contract = BcService.FindBeContractById(call.Id);
            Console.WriteLine($"Calling contract {contract?.Id}");
            //Filter to only give the correct outputs
            var notFiltredReturns = await CallAndLoopQueriesAsync(call, contract);

            var filtredReturns = new Dictionary<int, BeContractReturn>();
            var groupedOutputs = contract.Outputs.GroupBy(output => output.LookupInputId);
            groupedOutputs.ToList().ForEach(group =>
            {
                var beContractReturn = new BeContractReturn
                {
                    Outputs = new Dictionary<string, dynamic>()
                };
                group.ToList().ForEach(output =>
                {
                    var ret = notFiltredReturns[output.LookupInputId].Outputs.FirstOrDefault(o => o.Key.Equals(output.Key));
                    beContractReturn.Id = notFiltredReturns[output.LookupInputId].Id;
                    beContractReturn.Outputs.Add(ret.Key, ret.Value);
                });
                filtredReturns.Add(group.Key, beContractReturn);
            });
            
            filtredReturns.ToList().ForEach(ret =>
            {
                Console.WriteLine($"At LookupInputId {ret.Key} for contract {ret.Value.Id}");
                ret.Value.Outputs.ToList().ForEach(output =>
                {
                    Console.WriteLine($"\t{output.Key} - {output.Value}");
                });
            });

            return filtredReturns;
        }
    }
}
