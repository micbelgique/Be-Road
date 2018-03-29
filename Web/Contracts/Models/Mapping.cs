using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing a contract to call within a query
    /// </summary>
    public class Mapping
    {
        /// <summary>
        /// Name of the variable used as parameter to the contract called (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string InputKey { get; set; }
        /// <summary>
        /// Contract need to get the InputKey (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public BeContract Contract { get; set; }
        /// <summary>
        /// Key required to find the InputKey in the contract (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ContractKey { get; set; }
    }
}
