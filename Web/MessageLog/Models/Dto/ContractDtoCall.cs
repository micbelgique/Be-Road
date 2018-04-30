using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Models.Dto
{
    public class ContractDtoCall
    {
        public string ContractId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public bool Response { get; set; }
    }
}