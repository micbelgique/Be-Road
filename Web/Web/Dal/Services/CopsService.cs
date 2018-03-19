using System;
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage; // Namespace for Storage Client Library
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Azure Blobs
using Microsoft.WindowsAzure.Storage.File; // Namespace for Azure Files;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Models.PublicServiceData;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Web.Dal.Services
{
    public class CopsService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("dataexchange");
            PSDCar azure = null;
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                if (rootDir.Exists())
                {
                    CloudFile file = rootDir.GetFileReference("cops.json");
                    if (file.Exists())
                    {
                        var json = await file.DownloadTextAsync();
                        var lst = JsonConvert.DeserializeObject<List<PSDCar>>(json);
                        azure = lst.Where(car => car.Owner.Value.Equals(eid.FirstName)).FirstOrDefault();
                    }
                }
            }
            return azure;
        }
    }
}