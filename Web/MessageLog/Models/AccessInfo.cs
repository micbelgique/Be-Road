using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageLog.Models
{
    public class AccessInfo
    {
        public int Id { get; set; }
        public string NRID { get; set; }
        public string ContractId { get; set; }
        public string Name { get; set; }
        public string Justification { get; set; }
        public DateTime Date { get; set; }
    }
}