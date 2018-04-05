using Contracts.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing the contract that will be called by another contract
    /// </summary>
    [JsonConverter(typeof(BeContractQueryConverter))]
    public class Query
    {
        /// <summary>
        /// Key for entity framework
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Contract called
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public virtual BeContract Contract { get; set; }
        /// <summary>
        /// Id of the contract, used for EF
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// Set of mappings used to know what other contracts to call
        /// </summary>
        public virtual List<Mapping> Mappings { get; set; }
    }
}
