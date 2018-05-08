using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing a contract input
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Key for entity framework
        /// </summary>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Name of the variable (Required)
        /// </summary>
        [JsonProperty(Required = Newtonsoft.Json.Required.Always)]
        public string Key { get; set; }
        /// <summary>
        /// Type of the value (Required)
        /// </summary>
        [JsonProperty(Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; set; }
        /// <summary>
        /// Used to define whether the variable is required or not
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// Describes the variable role in the contract
        /// </summary>
        public string Description { get; set; }
    }
}
