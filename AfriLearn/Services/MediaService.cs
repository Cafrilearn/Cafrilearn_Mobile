using AfriLearn.Constants;
using AfriLearn.Models;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

namespace AfriLearn.Services
{
    class MediaService
    {
        public static async Task<byte[]> GetBlobAsync(Book book)
        {
            var account = CloudStorageAccount.Parse(AppConstants.BlobConnectionString);
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(BookType.MotherContainer);
            var relativePath = container.GetDirectoryReference(book.ContainerType);
            var blob = relativePath.GetBlockBlobReference(book.BookName);
            await blob.FetchAttributesAsync();
            byte[] blobBytes = new byte[blob.Properties.Length];
            await blob.DownloadToByteArrayAsync(blobBytes, 0);
            return blobBytes;
        }
    }
}
