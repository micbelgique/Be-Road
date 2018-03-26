using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class BeContract
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public List<Input> Inputs { get; set; }
        //Query
        //Ouputs
    }
}
