using AfriLearn.Constants;
using AfriLearn.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace AfriLearn.Services
{
    class MediaService
    {
        public static async Task<byte[]> GetBlobAsync(Book book)
        {
            var container = GetBlobContainer(BookType.MotherContainer);
            var relativePath = container.GetDirectoryReference(book.ContainerType);
            var blob = relativePath.GetBlockBlobReference(book.BookName);
            var blobExists = await blob.ExistsAsync().ConfigureAwait(false);
            if (blobExists)
            {
                await blob.FetchAttributesAsync();
                byte[] blobBytes = new byte[blob.Properties.Length];
                await blob.DownloadToByteArrayAsync(blobBytes, 0);
                return blobBytes;
            }
            return null;
        }
        public static CloudBlobContainer GetBlobContainer(string containerType)
        {
            var account = CloudStorageAccount.Parse(AppConstants.BlobConnectionString);
            var blobClient = account.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerType);
        }
    }
}
