using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class PSDUser : PublicServiceData
    {
        public Dictionary<string, PSDData> Datas { get; set; }
    }
}