﻿using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using PublicService.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicService.Models
{
    public class AzureUpload
    {
        public void UploadToAzure(PSContext db)
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
                    file.UploadTextAsync(json);
                }
            }
        }
    }
}