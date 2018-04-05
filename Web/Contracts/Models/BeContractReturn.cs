using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing what the contract returns
    /// </summary>
    public class BeContractReturn
    {
        /// <summary>
        /// Used to recognize the contract (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }
        /// <summary>
        /// Contains what the cocntracts returned
        /// </summary>
        public Dictionary<string, dynamic> Outputs { get; set; }
    }
}
