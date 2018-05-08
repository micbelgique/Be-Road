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
        /// Used to know in which query or contract we will take the inputs (Required)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int LookupInputId { get; set; }
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
