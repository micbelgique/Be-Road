using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Dto;

namespace Web.Models
{
    public class PublicServiceData
    {
        public string NRID { get; set; }
        public string ContractName { get; set; }
        public Dictionary<string, dynamic> Datas { get; set; }
        public List<AccessInfoDto> AccessInfos { get; set; }
    }
}