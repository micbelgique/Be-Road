using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Models
{
    public enum Determiner
    {
        Undefined = 0,
        BeContract = 1,
        AdapterServer = 2
    }

    public class Log
    {
        public int Id { get; set; }
        public Determiner Deter { get; set; }
        public string ContractId { get; set; }
        public string UseType { get; set; }
        public bool Response { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}