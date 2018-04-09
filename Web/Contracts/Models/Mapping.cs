using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing a contract to call within a query
    /// </summary>
    public class Mapping
    {
        /// <summary>
        /// Key for entity framework
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the variable used as parameter to the contract called (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string InputKey { get; set; }
        /// <summary>
        /// Used to know in which query or contract we will take the inputs (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int LookupInputId { get; set; }
        /// <summary>
        /// Used to know which input to take in the query or contract (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string LookupInputKey { get; set; }
    }
}
