using Contracts.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    [JsonConverter(typeof(BeContractOutputConverter))]
    public class Output
    {
        /// <summary>
        /// Key for entity framework
        /// </summary>
        [Key]
        public int Id { get; set; }
        [JsonProperty(Required = Required.Always)]
        public BeContract Contract { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string Key { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Type Type { get; set; }
        public string Description { get; set; }
    }
}
