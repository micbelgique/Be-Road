using LucidOcean.MultiChain;
using MessageLog.Helpers;
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

        public MultiChainConnection GetConnection()
        {
            return new MultiChainConnection()
            {
                Hostname = ConfigHelper.GetAppSetting("Hostname"),
                Port = Convert.ToInt32(ConfigHelper.GetAppSetting("Port")),
                Username = ConfigHelper.GetAppSetting("BC-Username"),
                Password = ConfigHelper.GetAppSetting("Password"),
                ChainName = ConfigHelper.GetAppSetting("ChainName"),
                BurnAddress = ConfigHelper.GetAppSetting("BurnAddress"),
                RootNodeAddress = ConfigHelper.GetAppSetting("RootNodeAddress")
            };
        }

        public MultiChainClient GetClient()
        {
            return new MultiChainClient(GetConnection());
        }
    }
}