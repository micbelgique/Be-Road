using Contracts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Logic
{
    public class Generators
    {
        public JSchema GenerateSchema()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            return generator.Generate(typeof(BeContract));
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
    }
}
