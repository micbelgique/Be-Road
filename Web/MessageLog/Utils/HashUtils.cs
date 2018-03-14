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

        public string HashAccessLogDto(AccessInfoDto access)
        {
            var hash = "SomePrefixBeforeHashSecure++";
            using (var sha = new SHA512Managed())
            {
                hash = Hash(sha, hash + access.Id.ToString());
                hash = Hash(sha, hash + access.Name);
                hash = Hash(sha, hash + access.Reason);
                hash = Hash(sha, hash + access.Date.ToString());
            }
            return hash;
        }

        string Hash(SHA512 sha, string value)
        {
            var result = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                sBuilder.Append(result[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }
}