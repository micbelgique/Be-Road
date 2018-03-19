using MessageLog.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MessageLog.Utils
{
    public sealed class HashUtils
    {

        #region Singleton
        //https://msdn.microsoft.com/en-us/library/ff650316.aspx
        //Multithread safety singleton
        private static volatile HashUtils instance;
        private static object syncRoot = new Object();

        private HashUtils() { }

        public static HashUtils Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new HashUtils();
                    }
                }

                return instance;
            }
        }
        #endregion

        public string HashAccessLogDtoToString(AccessInfoDto access)
        {
            var result = HashAccessLogDto(access);
            StringBuilder sb = new StringBuilder();
            foreach(var r in result)
            {
                sb.Append(r.ToString("x2"));
            }
            return sb.ToString();
        }

        public byte[] HashAccessLogDto(AccessInfoDto access)
        {
            var hash = Encoding.UTF8.GetBytes("SomePrefixBeforeHashSecure++");
            using (var sha = new SHA512Managed())
            {
                hash = Hash(sha, hash.Concat(Encoding.UTF8.GetBytes(access.Id.ToString())).ToArray());
                hash = Hash(sha, hash.Concat(Encoding.UTF8.GetBytes(access.Name)).ToArray());
                hash = Hash(sha, hash.Concat(Encoding.UTF8.GetBytes(access.Reason)).ToArray());
                hash = Hash(sha, hash.Concat(Encoding.UTF8.GetBytes(access.Date.ToString())).ToArray());
            }
            return hash;
        }

        byte[] Hash(SHA512 sha, byte[] value)
        {
            var result = sha.ComputeHash(value);
            return result;
        }
    }
}