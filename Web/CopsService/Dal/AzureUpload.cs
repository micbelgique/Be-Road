using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using PublicService.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PublicService.Dal
{
    public class AzureUpload
    {
        public async Task UploadToAzureAsync(PSContext db)
        {
            var json = JsonConvert.SerializeObject(db.Users.ToList());
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("files");
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                if (rootDir.Exists())
                {
                    CloudFile file = rootDir.GetFileReference("users.json");
                    await file.UploadTextAsync(json);
                }
            }
        }
    }
}