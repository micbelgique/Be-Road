using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Models
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
