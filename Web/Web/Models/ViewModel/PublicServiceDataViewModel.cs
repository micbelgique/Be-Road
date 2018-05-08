using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModel
{
    public class PublicServiceDataViewModel
    {
        public string NRID { get; set; }
        public string ContractName { get; set; }
        public Dictionary<string, dynamic> Datas { get; set; }
        public List<AccessInfoViewModel> AccessInfos { get; set; }
    }
}