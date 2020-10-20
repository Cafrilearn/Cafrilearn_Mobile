using AfriLearn.Constants;
using AfriLearn.Models;
using AfriLearnBackend.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AfriLearnBackend.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IConfiguration _storageCredentials;
        public BooksRepository(IConfiguration storageCredentials)
        {
            _storageCredentials = storageCredentials;
        }

        public async Task<List<string>> GetFilesListAsync(string  relativeContainerPath)
        {
            var container = GetBlobContainer(BookType.MotherContainer);
            var referenceContainer = container.GetDirectoryReference(relativeContainerPath);
            var allBlobsList = new List<string>();
            BlobContinuationToken token = null;
            do
            {
                var result = await referenceContainer.ListBlobsSegmentedAsync(token).ConfigureAwait(false);
                if (result.Results.Count() > 0)
                {
                    var blobs = result.Results.Cast<CloudBlockBlob>().Select(b => b.Name);
                    allBlobsList.AddRange(blobs);
                }
                token = result.ContinuationToken;
            }
            while (token != null);
            return allBlobsList;
        }

        public async Task<List<string>> GetAllBookNames()
        {
            var scienceBooks = await GetFilesListAsync(BookType.Science);
            var englishBooks = await GetFilesListAsync(BookType.English);
            var mathBooks = await GetFilesListAsync(BookType.Mathematics);
            var kiswahiliBooks = await GetFilesListAsync(BookType.Kiswahili);
            var reliousEducationBooks = await GetFilesListAsync(BookType.ReligiousEducation);
            var socialStudiesBooks = await GetFilesListAsync(BookType.SocialStudies);
            var agricultureBooks = await GetFilesListAsync(BookType.Agriculture);
            var biologyBooks = await GetFilesListAsync(BookType.Biology);
            var bussinessBooks = await GetFilesListAsync(BookType.BusinessStudies);
            var chemistryBooks = await GetFilesListAsync(BookType.Chemistry);
            var computerScienceBooks = await GetFilesListAsync(BookType.ComputerStudies);
            var englishSecBooks = await GetFilesListAsync(BookType.English);
            var geographyBooks = await GetFilesListAsync(BookType.Geography);
            var historyBooks = await GetFilesListAsync(BookType.History);
            var homeScienceBooks = await GetFilesListAsync(BookType.HomeScience);
            var kiswahiliSecBooks = await GetFilesListAsync(BookType.Kiswahili);
            var mathematicsBooks = await GetFilesListAsync(BookType.Mathematics);
            var physicsBooks = await GetFilesListAsync(BookType.Physics);
            var religiousEducationBoooks = await GetFilesListAsync(BookType.ReligiousEducation);
            var setBooks = await GetFilesListAsync(BookType.SetBooks);
           
            var allBooks = new List<string>();

            allBooks.AddRange(scienceBooks);
            allBooks.AddRange(englishBooks);
            allBooks.AddRange(mathBooks);
            allBooks.AddRange(kiswahiliBooks);
            allBooks.AddRange(reliousEducationBooks);
            allBooks.AddRange(socialStudiesBooks);
            allBooks.AddRange(agricultureBooks);
            allBooks.AddRange(biologyBooks);
            allBooks.AddRange(bussinessBooks);
            allBooks.AddRange(chemistryBooks);
            allBooks.AddRange(computerScienceBooks);
            allBooks.AddRange( englishSecBooks);
            allBooks.AddRange(geographyBooks);
            allBooks.AddRange(historyBooks);
            allBooks.AddRange(homeScienceBooks);
            allBooks.AddRange(kiswahiliSecBooks);
            allBooks.AddRange(mathematicsBooks);
            allBooks.AddRange(physicsBooks);
            allBooks.AddRange(religiousEducationBoooks);
            allBooks.AddRange(setBooks);

            return allBooks;
        }

        public async Task<Stream> GetBlobAsync(Book book)
        {
            Stream streamData;
            var container = GetBlobContainer(BookType.MotherContainer);
            var  relativePath = container.GetDirectoryReference(book.ContainerType);
            var blob = relativePath.GetBlockBlobReference(book.BookName);
            var blobExists = await blob.ExistsAsync().ConfigureAwait(false);
            if (blobExists)
            {
                await blob.FetchAttributesAsync();
                byte[] blobBytes = new byte[blob.Properties.Length];
                await blob.DownloadToByteArrayAsync(blobBytes, 0);
                streamData = new MemoryStream(blobBytes);
                return streamData;
            }
            return null;
        }

        public CloudBlobContainer GetBlobContainer(string containerType)
        {
            var connStr = _storageCredentials.GetSection("BlobStorageDetails").GetSection("ConnectionString").Value;
            var account = CloudStorageAccount.Parse(connStr);
            var blobClient = account.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerType);
        }

        public Task GetBook(string bookType, string bookFormat)
        {
            return null;
        }        
    }
}
