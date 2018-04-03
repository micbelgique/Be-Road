using Proxy.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Converters
{
    public class BeContractQueryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Query));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var query = new Query()
            {
                //Take only the id and not the object it self
                Contract = new BeContract() { Id = (string)obj["Contract"] },
                Mappings = new List<Mapping>()
            };

            obj["Mappings"].Children().ToList().ForEach(token =>
            {
                query.Mappings.Add(new Mapping()
                {
                    Contract = new BeContract() { Id = (string)token["Contract"] },
                    ContractKey = (string)token["ContractKey"],
                    InputKey = (string)token["InputKey"],
                });
            });

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var converters = serializer.Converters.Where(x => !(x is BeContractQueryConverter)).ToArray();
            //Create a whole new JObject for exception: Self referencing loop throws by JObject.FromObject
            var query = value as Query;
            var jObject = new JObject();
            var jMappings = new JArray();
            query?.Mappings?.ForEach(m =>
            {
                var jMapping = new JObject
                {
                    new JProperty("InputKey", m.InputKey),
                    new JProperty("Contract", m.Contract?.Id),
                    new JProperty("ContractKey", m.ContractKey)
                };
                jMappings.Add(jMapping);
            });
            jObject.AddFirst(new JProperty("Contract", query?.Contract?.Id));
            jObject.Add(new JProperty("Mappings", jMappings));
            jObject.WriteTo(writer, converters);
        }
    }
}
