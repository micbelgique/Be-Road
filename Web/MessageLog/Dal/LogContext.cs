using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MessageLog.Dal
{
    public class LogContext : DbContext
    {
        public LogContext() : base()
        {

        }

        public LogContext(string context): base(context)
        {

        }

        public System.Data.Entity.DbSet<MessageLog.Models.AccessInfo> AccessLogs { get; set; }
        public System.Data.Entity.DbSet<MessageLog.Models.Log> Logs { get; set; }
    }
}
