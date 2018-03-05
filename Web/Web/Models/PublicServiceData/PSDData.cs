using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class PSDData
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual List<PSDAccessInfo> AccessInfos { get; set; }
    }
}