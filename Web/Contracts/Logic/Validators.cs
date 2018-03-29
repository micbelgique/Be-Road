using Contracts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.Validation;
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
        public JsonSchema4 Schema { get; set; }

        public Validators()
        {
            Generators = new Generators();
        }

        public async Task<Boolean> ValidateBeContract(BeContract contract)
        {
            try
            {
                if(Schema == null)
                    Schema = await Generators.GenerateSchema();

                var json = Generators.SerializeBeContract(contract);
                var errors = Schema.Validate(json);
                var str = "";
                foreach(var e in errors.ToList())
                    str += e.ToString();

                if (errors.Count > 0) {
                    throw new BeContractException(errors.ToString()) {BeContract = contract };
                }
                else
                {
                    return true;
                }
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
                    if (value.GetType().Name != input.Type)
                    {
                        throw new BeContractException($"The contract expects {input.Type} but {value.GetType().Name} was found")
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
        }

        public void ValidateBeContractReturn(BeContract contract, BeContractReturn ret)
        {
            if (!contract.Id.Equals(ret.Id))
            {
                throw new BeContractException("Contract's do not have the same ID")
                {
                    BeContract = contract,
                    BeContractReturn = ret
                };
            }

            //Test outputs
            contract.Outputs.ForEach(output =>
            {
                if (ret.Outputs.TryGetValue(output.Key, out dynamic value))
                {
                    //Check if the dynamic is a int64
                    if (value.GetType() == typeof(Int64))
                    {
                        value = (int)value;
                        ret.Outputs[output.Key] = value;
                    }


                    //Check the types
                    if (value.GetType().Name != output.Type)
                    {
                        throw new BeContractException($"The contract expects {output.Type} but {value.GetType().Name} was found")
                        {
                            BeContract = contract,
                            BeContractReturn = ret
                        };
                    }
                }
                else
                {
                    //Key isn't find 
                    throw new BeContractException($"No key was found for {output.Key}")
                    {
                        BeContract = contract,
                        BeContractReturn = ret
                    };
                }
            });
        }
    }
}
