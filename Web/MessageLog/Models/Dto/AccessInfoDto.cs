using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Models.Dto
{
    public class AccessInfoDto
    {
        public int Id { get; set; }
        public Boolean IsReliable { get; set; }
        public string Name { get; set; }
        public string Justification { get; set; }
        public DateTime Date { get; set; }
    }
}