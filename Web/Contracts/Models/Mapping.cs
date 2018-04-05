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
        /// Contract need to get the InputKey (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public virtual BeContract Contract { get; set; }
        /// <summary>
        /// Id of the contract, used for EF
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// Key required to find the InputKey in the contract (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ContractKey { get; set; }
    }
}
