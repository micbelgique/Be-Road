using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PublicService
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContractId { get; set; }
        public List<String> ContractNames { get; set; }
        public string Description { get; set; }
        public string ImageURI { get; set; }
    }
}