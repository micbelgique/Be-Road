using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models
{
    /// <summary>
    /// Class representing an adapter server
    /// </summary>
    public class AdapterServer
    {
        /// <summary>
        /// Id of the ADS, used for EF
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Names of the contracts handled by the Information System
        /// </summary>
        public virtual List<ContractName> ContractNames { get; set; }
        /// <summary>
        /// Name of the Information System
        /// </summary>
        public string ISName { get; set; }
        /// <summary>
        /// URL of the adapter server API
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Root of the API to call 
        /// </summary>
        public string Root { get; set; }
    }
}
