using Contracts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Logic
{
    public class Validators
    {
        public Generators Generators { get; set; }
        public JSchema Schema { get; set; }

        public Validators()
        {
            Generators = new Generators();
            Schema = Generators.GenerateSchema();
        }

        public Boolean ValidateBeContract(BeContract contract)
        {
            try
            {
                var jObj = JObject.FromObject(contract);
                return jObj.IsValid(Schema);
            }
            catch (JsonSerializationException ex)
            {
                throw new BeContractException(ex.Message) { BeContract = contract };
            }
        }

        public void ValidateBeContractCall(BeContract contract, BeContractCall call)
        {
            if (!contract.Id.Equals(call.Id))
            {
                throw new BeContractException("Contract's do not have the same ID")
                {
                    BeContract = contract,
                    BeContractCall = call
                };
            }

            //Test inputs
            contract.Inputs.ForEach(input =>
            {
                if (call.Inputs.TryGetValue(input.Key, out dynamic value))
                {
                    //Check if the dynamic is a int64
                    if (value.GetType() == typeof(Int64))
                    {
                        value = (int)value;
                        call.Inputs[input.Key] = value;
                    }


                    //Check the types
                    if (value.GetType() != input.Type)
                    {
                        throw new BeContractException($"The contract expects {input.Type.Name} but {value.GetType().Name} was found")
                        {
                            BeContract = contract,
                            BeContractCall = call
                        };
                    }
                }
                else if(input.Required)
                {
                    //Key isn't find and is required
                    throw new BeContractException($"No key was found for {input.Key} and it is required")
                    {
                        BeContract = contract,
                        BeContractCall = call
                    };
                }
            });
            //Test outputs ?
        }
    }
}
