using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Models
{
    /// <summary>
    /// Class representing a contract
    /// </summary>
    public class BeContract
    {
        /// <summary>
        /// Used to recognize the contract (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }
        /// <summary>
        /// Describes the contract goal
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Specifies the version of the contract
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Values used by the contract
        /// </summary>
        public virtual List<Input> Inputs { get; set; }
        /// <summary>
        /// Other contracts to call to get the needed data
        /// </summary>
        public virtual List<Query> Queries { get; set; }
        /// <summary>
        /// Values returned by the contract
        /// </summary>
        public virtual List<Output> Outputs { get; set; }
    }
}
