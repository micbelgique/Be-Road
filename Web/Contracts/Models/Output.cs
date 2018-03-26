using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class Output
    {
        [JsonProperty(Required = Required.Always)]
        public string Key { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Type Type { get; set; }
        public string Description { get; set; }
    }
}
