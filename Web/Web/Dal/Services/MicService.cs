using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Models.PublicServiceData;

namespace Web.Dal.Services
{
    public class MicService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("files");
            PSDMic mic = null;
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                if (rootDir.Exists())
                {
                    CloudFile file = rootDir.GetFileReference(ps.Url);
                    if (file.Exists())
                    {
                        var json = await file.DownloadTextAsync();
                        var lst = JsonConvert.DeserializeObject<List<PSDMic>>(json);
                        mic = lst.Where(model => model.FirstName.Name.Equals(eid.FirstName)).FirstOrDefault();
                    }
                }
            }
            return mic;
        }
    }
}