using Contracts.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing a contract output
    /// </summary>
    [JsonConverter(typeof(BeContractOutputConverter))]
    public class Output
    {
        /// <summary>
        /// Key for entity framework
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Contract where the output comes from (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public virtual BeContract Contract { get; set; } 
        /// <summary>
        /// Id of the contract, used for EF
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// Name of the variable returned (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Key { get; set; }
        /// <summary>
        /// Type of the variable returned (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Type { get; set; }
        /// <summary>
        /// Describes the returned variable
        /// </summary>
        public string Description { get; set; }
    }
}
