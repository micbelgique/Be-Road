using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModel
{
    public class AccessInfoViewModel
    {
        public bool IsReliable { get; set; }
        public string Name { get; set; }
        public string Justification { get; set; }
        public string Date { get; set; }
        public int Total { get; set; }
    }
}