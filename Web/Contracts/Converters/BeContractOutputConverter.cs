using Contracts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Converters
{
    public class BeContractOutputConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Output));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var output = new Output
            {
                //Take only the id and not the object it self
                Contract = new BeContract() { Id = (string)obj["Contract"] },
                Description = (string)obj["Description"],
                Key = (string)obj["Key"],
                Type = (string)obj["Type"]
            };
            return output;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var converters = serializer.Converters.Where(x => !(x is BeContractOutputConverter)).ToArray();
            //Create a whole new JObject for exception: Self referencing loop throws by JObject.FromObject
            var output = value as Output;
            var jObject = new JObject();
            jObject.AddFirst(new JProperty("Key", output.Key));
            jObject.AddFirst(new JProperty("Description", output?.Description));
            jObject.AddFirst(new JProperty("Type", output.Type));
            jObject.AddFirst(new JProperty("Contract", output.Contract?.Id));
            jObject.WriteTo(writer, converters);
        }
    }
}
