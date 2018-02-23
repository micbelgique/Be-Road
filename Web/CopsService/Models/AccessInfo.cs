using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class AccessInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}