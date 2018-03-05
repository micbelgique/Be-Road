using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Models.PublicServiceData;

namespace Web.Dal.Services
{
    public class PSService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("files");
            PSDUser user = new PSDUser();
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                if (rootDir.Exists())
                {
                    CloudFile file = rootDir.GetFileReference(ps.Url);
                    if (file.Exists())
                    {
                        var jsonTxt = await file.DownloadTextAsync();
                        var jArray = JArray.Parse(jsonTxt);
                        var child = jArray.FirstOrDefault(
                            o => String.Compare(o["FirstName"]["Value"].ToString().Trim(), eid.FirstName, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 &&
                            String.Compare(o["LastName"]["Value"].ToString().Trim(), eid.LastName, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);
                        user.Datas = new Dictionary<string, PSDData>();
                        foreach (JProperty x in child)
                        {
                            var name = x.Name;
                            var value = x.Value.ToObject<PSDData>();
                            user.Datas.Add(name, value);
                        }
                    }
                }
            }
            return user;
        }
    }
}