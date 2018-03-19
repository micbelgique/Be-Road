using System;
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage; // Namespace for Storage Client Library
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Azure Blobs
using Microsoft.WindowsAzure.Storage.File; // Namespace for Azure Files
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Models.PublicServiceData;

namespace Web.Dal.Services
{
    public class MockAzureService : IService
    {
        public async Task<PublicServiceData> GetDataOfAsync(PublicService ps, EidCard eid)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("dataexchange");
            MockPSDAzure azure = new MockPSDAzure();
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                //CloudFileDirectory sampleDir = rootDir.GetDirectoryReference("CustomLogs");

                if (rootDir.Exists())
                {
                    CloudFile file = rootDir.GetFileReference(ps.Url);
                    if (file.Exists())
                    {
                        azure.TextField = await file.DownloadTextAsync();
                    }
                }
            }
            return azure;
        }
    }
}