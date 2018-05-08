using Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageLog.Models.Dto
{
    public class ContractDtoCall
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserType { get; set; }
        public bool Response { get; set; }
        public Dictionary<string, dynamic> Inputs { get; set; }
    }
}