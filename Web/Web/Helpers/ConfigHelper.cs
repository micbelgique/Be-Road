using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public class ConfigHelper
    {
        public static string GetAppSetting(string appSettingName)
        {
            var cs = GetSettingFromEnvironmentVariable(appSettingName) ??
                ConfigurationManager.AppSettings[appSettingName];
            return cs;
        }

        public static string GetConnectionString(string connectionStringName)
        {
            return GetSettingFromEnvironmentVariable(connectionStringName) ??
                ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public static string GetSettingFromEnvironmentVariable(string configKey)
        {
            return Environment.GetEnvironmentVariable(configKey, EnvironmentVariableTarget.Process) ??
                Environment.GetEnvironmentVariable(configKey, EnvironmentVariableTarget.User) ??
                Environment.GetEnvironmentVariable(configKey, EnvironmentVariableTarget.Machine);
        }
    }
}