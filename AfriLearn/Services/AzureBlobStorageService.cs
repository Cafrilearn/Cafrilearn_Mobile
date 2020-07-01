using AfriLearn.Constants;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AfriLearn.Services
{
    static class AzureBlobStorageService
    {
        public static  CloudBlobContainer GetBlobContainer(string containerType)
        {
            // all the docs is at this uri 
            // https://docs.microsoft.com/en-gb/xamarin/xamarin-forms/data-cloud/azure-services/azure-storage
            // get container references, so as to start  performing actions for the blobs
            var account = CloudStorageAccount.Parse(AzureBlobStorageConstants.ConnectionString);
            var blobClient = account.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerType.ToString());
        }

        public static async Task<Stream>  GetBlobAsync(string containerType, string blobName)
        {
            // blobs are the actual files being downloaded as binary
           Stream streamData = null;
            var container = GetBlobContainer(containerType);
            var blob = container.GetBlobReference(blobName);
            var blobExists = await blob.ExistsAsync().ConfigureAwait(false);
            if (blobExists)
            {
                await blob.FetchAttributesAsync();
                byte[] blobBytes = new byte[blob.Properties.Length];
                await blob.DownloadToByteArrayAsync(blobBytes, 0);
                streamData =  new MemoryStream(blobBytes);
                return  streamData;
            }

            return null;
            
        }
        public static async Task<List<string>> GetFilesListAsync(string containerType)
        {
            var container = GetBlobContainer(containerType);

            var allBlobsList = new List<string>();
            BlobContinuationToken token = null;

            do
            {
                var result = await container.ListBlobsSegmentedAsync(token).ConfigureAwait(false);
                if (result.Results.Count() > 0)
                {
                    var blobs = result.Results.Cast<CloudBlockBlob>().Select(b => b.Name);
                    allBlobsList.AddRange(blobs);
                }
                token = result.ContinuationToken;
            } while (token != null);
            return allBlobsList;
        }
    }
}
