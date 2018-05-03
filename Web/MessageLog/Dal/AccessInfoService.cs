using LucidOcean.MultiChain.Util;
using MessageLog.Models;
using MessageLog.Models.Dto;
using MessageLog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MessageLog.Dal
{
    public class AccessInfoService
    {
        private LogContext db;

        public AccessInfoService(LogContext db)
        {
            this.db = db;
        }

        public async Task LogAccessInfo(AccessInfo accessInfo)
        {
            var client = MultichainUtils.Instance.GetClient();
            var hash = HashUtils.Instance.HashAccessLogDtoToString(accessInfo);
            var tx = await client.Stream.PublishAsync("accesslogs", "Dev-Demo-2", Utility.HexToByteArray(hash));
            
            db.AccessLogs.Add(accessInfo);
            var accessInfoHash = new AccessInfoHash
            {
                AccessInfo = accessInfo,
                Hash = hash,
                TransactionAddress = tx.Result
            };

            db.AccessLogHashs.Add(accessInfoHash);
        }
        
    }
}