using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual List<AccessInfo> AccessInfos { get; set; }
    }
}