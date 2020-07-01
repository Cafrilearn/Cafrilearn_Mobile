using AfriLearn.Constants;
using Akavache;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AfriLearn.Services
{
     class AzureBlobStorageService
    {
        public static  CloudBlobContainer GetBlobContainer(string containerType)
        {
            // all the docs is at this uri 
            // https://docs.microsoft.com/en-gb/xamarin/xamarin-forms/data-cloud/azure-services/azure-storage
            // get container references, so as to start  performing actions for the blobs
            var account = CloudStorageAccount.Parse(AzureBlobStorageConstants.ConnectionString);
            var blobClient = account.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerType);
        }

        public static async Task<Stream>  GetBlobAsync(string containerType, string blobName)
        {
            // blobs are the actual files being downloaded as binary
            Stream streamData;
            var container = GetBlobContainer(containerType);
            var blob = container.GetBlobReference(blobName);
            var blobExists = await blob.ExistsAsync().ConfigureAwait(false);
            if (blobExists)
            {
                await blob.FetchAttributesAsync();
                byte[] blobBytes = new byte[blob.Properties.Length];
                await blob.DownloadToByteArrayAsync(blobBytes, 0);
                streamData =  new MemoryStream(blobBytes);
               // await BlobCache.LocalMachine.InsertObject<Stream>(blobName, streamData);
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
        public static async void GetAllBookNames()
        {
            try
            {
                // check if the book names are already stored locally, download them otherwise
                var allbooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
             
            }
            catch (System.Exception)
            {
                // get book names from the containers
                var scienceBooks = await AzureBlobStorageService.GetFilesListAsync(BookType.Science);
                var englishBooks = await AzureBlobStorageService.GetFilesListAsync(BookType.English);
                var mathBooks = await AzureBlobStorageService.GetFilesListAsync(BookType.Mathematics);
                var kiswahiliBooks = await AzureBlobStorageService.GetFilesListAsync(BookType.Kiswahili);
                var reliousEducationBooks = await AzureBlobStorageService.GetFilesListAsync(BookType.ReligiousEducation);
                var socialStudiesBooks = await AzureBlobStorageService.GetFilesListAsync(BookType.SocialStudies);

                // store all the names in one list
                var allBooks = new List<string>();
                allBooks.AddRange(scienceBooks);
                allBooks.AddRange(englishBooks);
                allBooks.AddRange(mathBooks);
                allBooks.AddRange(kiswahiliBooks);
                allBooks.AddRange(reliousEducationBooks);
                allBooks.AddRange(socialStudiesBooks);

                await BlobCache.LocalMachine.InsertObject<List<string>>("allBookNames", allBooks);
            }

        }
    }
}
