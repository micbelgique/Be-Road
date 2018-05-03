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
        const string BC_KEY = "Dev-Demo-3";
        private LogContext db;

        public AccessInfoService(LogContext db)
        {
            this.db = db;
        }

        public async Task LogAccessInfo(AccessInfo accessInfo)
        {
            // Need to save to the db before hashing, otherwise the ID will not be the same
            db.AccessLogs.Add(accessInfo);
            await db.SaveChangesAsync();
            
            var client = MultichainUtils.Instance.GetClient();
            var hash = HashUtils.Instance.HashAccessLogDtoToString(accessInfo);
            var tx = await client.Stream.PublishAsync("accesslogs", BC_KEY, Utility.HexToByteArray(hash));
            
            db.AccessLogHashs.Add(new AccessInfoHash
            {
                AccessInfo = accessInfo,
                Hash = hash,
                TransactionAddress = tx.Result
            });
        }

        public async Task<bool> IsReliableAsync(AccessInfo log)
        {
            var accessHash = db.AccessLogHashs.FirstOrDefault(hsh => hsh.AccessInfo.Id == log.Id);
            //Doesn't exist
            if (accessHash == null) return false;

            //Not the same hash
            var hash = HashUtils.Instance.HashAccessLogDtoToString(log);
            if (!accessHash.Hash.Equals(hash)) return false;

            //Check the hash in the Blockchain 
            var client = MultichainUtils.Instance.GetClient();
            var tx = await client.Transaction.GetRawTransactionVerboseAsync(accessHash.TransactionAddress);
            if(tx.Result.Data.Count == 1)
            {
                hash = tx.Result.Data[0].ToString();
                if (!accessHash.Hash.Equals(hash)) return false;
            }

            return true;
        }
    }
}