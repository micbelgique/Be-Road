using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class BeContractReturn
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }
        public Dictionary<string, dynamic> Outputs { get; set; }
    }
}
