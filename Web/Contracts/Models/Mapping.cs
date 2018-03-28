using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class Mapping
    {
        [JsonProperty(Required = Required.Always)]
        public string InputKey { get; set; }

        [JsonProperty(Required = Required.Always)]
        public BeContract Contract { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ContractKey { get; set; }
    }
}
