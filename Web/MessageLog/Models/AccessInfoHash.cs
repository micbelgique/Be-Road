using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageLog.Models
{
    public class AccessInfoHash
    {
        public int Id { get; set; }
        public virtual AccessInfo AccessInfo { get; set; }
        public string Hash { get; set; }
        public string TransactionAddress { get; set; }
    }
}