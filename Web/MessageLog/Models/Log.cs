using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Models
{
    public enum Determiner
    {
        BeContract,
        AdapterServer
    }

    public class Log
    {
        public int Id { get; set; }
        public Determiner Deter { get; set; }
        public string ContractId { get; set; }
        public bool Response { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
        public AccessInfo Access { get; set; }
    }
}