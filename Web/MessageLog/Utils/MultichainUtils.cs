using LucidOcean.MultiChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLog.Utils
{
    public class MultichainUtils
    {
        #region Singleton
        //https://msdn.microsoft.com/en-us/library/ff650316.aspx
        //Multithread safety singleton
        private static volatile MultichainUtils instance;
        private static object syncRoot = new Object();

        private MultichainUtils() { }

        public static MultichainUtils Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MultichainUtils();
                    }
                }

                return instance;
            }
        }
        #endregion

        private MultiChainConnection GetConnection()
        {
            return new MultiChainConnection()
            {
                Hostname = System.Configuration.ConfigurationManager.AppSettings["Hostname"],
                Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"]),
                Username = System.Configuration.ConfigurationManager.AppSettings["Username"],
                Password = System.Configuration.ConfigurationManager.AppSettings["Password"],
                ChainName = System.Configuration.ConfigurationManager.AppSettings["ChainName"],
                BurnAddress = System.Configuration.ConfigurationManager.AppSettings["BurnAddress"],
                RootNodeAddress = System.Configuration.ConfigurationManager.AppSettings["RootNodeAddress"]
            };
        }

        public MultiChainClient GetClient()
        {
            return new MultiChainClient(GetConnection());
        }
    }
}