using Contracts.Converters;
using Contracts.Models;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Logic
{
    public class Generators
    {
        public async Task<JsonSchema4> GenerateSchema()
        {
            var schema = await JsonSchema4.FromTypeAsync<BeContract>();
            return schema;
        }

        public BeContractCall GenerateBeContractCall(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<BeContractCall>(json);
            }
            catch(JsonSerializationException ex)
            {
                throw new BeContractException(ex.Message);
            }
        }

        public string SerializeBeContract(BeContract contract)
        {
            return JsonConvert.SerializeObject(contract, Formatting.Indented, new BeContractInputConverter(), new BeContractOutputConverter());
        }
        public BeContract DeserializeBeContract(string json)
        {
            return JsonConvert.DeserializeObject<BeContract>(json, new BeContractInputConverter(), new BeContractOutputConverter());
        }
    }
}
