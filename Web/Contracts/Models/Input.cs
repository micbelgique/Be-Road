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
    public class Input
    {
        /// <summary>
        /// Key for entity framework
        /// </summary>
        [Key]
        public int Id { get; set; }
        [JsonProperty(Required = Newtonsoft.Json.Required.Always)]
        public string Key { get; set; }
        [JsonProperty(Required = Newtonsoft.Json.Required.Always)]
        public Type Type { get; set; }
        public Boolean Required { get; set; }
        public string Description { get; set; }
    }
}
