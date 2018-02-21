using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class PSDAccessInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}