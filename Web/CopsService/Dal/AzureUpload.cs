﻿using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using PublicService.Dal;
using PublicService.Models.Dto;
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
            var users = db.Users;
            var dtos = users.Select(user => new ApplicationUserDto()
            {
                FirstName = new DataDto() { Value = user.FirstName.Value, AccessInfos = user.FirstName.AccessInfos },
                LastName = new DataDto() { Value = user.LastName.Value, AccessInfos = user.LastName.AccessInfos },
                BirthDate = new DataDto() { Value = user.BirthDate.Value, AccessInfos = user.BirthDate.AccessInfos },
                Locality = new DataDto() { Value = user.Locality.Value, AccessInfos = user.Locality.AccessInfos },
                Nationality = new DataDto() { Value = user.Nationality.Value, AccessInfos = user.Nationality.AccessInfos },
                PhotoUrl = new DataDto() { Value = user.PhotoUrl.Value, AccessInfos = user.PhotoUrl.AccessInfos },
                ExtraInfo = new DataDto() { Value = user.ExtraInfo.Value, AccessInfos = user.ExtraInfo.AccessInfos },
                EmailAddress = new DataDto() { Value = user.EmailAddress.Value, AccessInfos = user.EmailAddress.AccessInfos }
            });
            var json = JsonConvert.SerializeObject(dtos.ToList());
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