using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PublicService.Helpers
{
    public class ConfigHelper
    {
        /// <summary>
        /// Get the url of a service
        /// If there's no data in the config, is will use the docker hostname
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static string GetServiceUrl(string service)
        {
            var url = GetAppSetting(service);
            if (string.IsNullOrWhiteSpace(url))
                return $"http://{service}/";
            return url;
        }

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