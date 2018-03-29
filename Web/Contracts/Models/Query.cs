using Contracts.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public BeContract Contract { get; set; }
        /// <summary>
        /// Set of mappings used to know what other contracts to call
        /// </summary>
        public List<Mapping> Mappings { get; set; }
    }
}
