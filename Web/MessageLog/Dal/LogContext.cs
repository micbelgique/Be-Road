using MessageLog.Models;
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

        public DbSet<AccessInfo> AccessLogs { get; set; }
        public DbSet<AccessInfoHash> AccessLogHashs { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
