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
    public class BeContractInputConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Input));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var input = new Input
            {
                Description = (string)obj["Description"],
                Key = (string)obj["Key"],
                Required = obj["Required"].ToObject<bool>(),
                Type = Type.GetType($"System.{(string)obj["Type"]}")
            };
            return input;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var converters = serializer.Converters.Where(x => !(x is BeContractInputConverter)).ToArray();
            var jObject = JObject.FromObject(value);
            var type = jObject.SelectToken("Type");
            jObject.Remove("Type");
            jObject.AddFirst(new JProperty("Type", type.ToObject<Type>().Name));
            jObject.WriteTo(writer, converters);
        }
    }
}
