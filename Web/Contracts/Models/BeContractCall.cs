using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing a contract call
    /// </summary>
    public class BeContractCall
    {
        /// <summary>
        /// Used to recognize the contract (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }
        /// <summary>
        /// Passes the parameters to the contract
        /// </summary>
        public Dictionary<string, dynamic> Inputs { get; set; }
    }
}
