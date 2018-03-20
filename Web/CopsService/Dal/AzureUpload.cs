using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using PublicService.Dal;
using PublicService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
                NRID = user.NRID,
                FirstName = new DataDto() { Id = (int?)user.FirstName.Id ?? 0, Value = user.FirstName.Value, AccessInfos = user.FirstName.AccessInfos },
                LastName = new DataDto() { Id = (int?)user.LastName.Id ?? 0, Value = user.LastName.Value, AccessInfos = user.LastName.AccessInfos },
                BirthDate = new DataDto() { Id = (int?)user.BirthDate.Id ?? 0, Value = user.BirthDate.Value, AccessInfos = user.BirthDate.AccessInfos },
                Locality = new DataDto() { Id = (int?)user.Locality.Id ?? 0, Value = user.Locality.Value, AccessInfos = user.Locality.AccessInfos },
                Nationality = new DataDto() { Id = (int?)user.Nationality.Id ?? 0, Value = user.Nationality.Value, AccessInfos = user.Nationality.AccessInfos },
                PhotoUrl = new DataDto() { Id = (int?)user.PhotoUrl.Id ?? 0, Value = user.PhotoUrl.Value, AccessInfos = user.PhotoUrl.AccessInfos },
                ExtraInfo = new DataDto() { Id = (int?)user.ExtraInfo.Id ?? 0, Value = user.ExtraInfo.Value, AccessInfos = user.ExtraInfo.AccessInfos },
                EmailAddress = new DataDto() { Id = (int?)user.EmailAddress.Id ?? 0, Value = user.EmailAddress.Value, AccessInfos = user.EmailAddress.AccessInfos }
            });
            var json = JsonConvert.SerializeObject(dtos.ToList());
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("dataexchange");
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                if (rootDir.Exists())
                {
                    CloudFile file = rootDir.GetFileReference("users.json");
                    await file.UploadTextAsync(json);
                    //Do we need to check the response ?
                    await SendToAPIAsync(dtos.ToList());
                }
            }
        }

        public async Task<HttpResponseMessage> SendToAPIAsync(List<ApplicationUserDto> list)
        {
            using (var client = new HttpClient())
            {
                //It's hardcoded for the moment
                //This will be changed when the X-Road Security Server exist
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ApiUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PostAsJsonAsync("api/AccessLog", list);
            }
        }
    }
}