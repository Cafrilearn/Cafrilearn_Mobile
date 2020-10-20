using AfriLearn.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AfriLearnBackend.IRepositories
{
    public interface IBooksRepository
    {
        Task GetBook(string bookType, string bookFormat);
        Task<Stream> GetBlobAsync(Book book);
        Task<List<string>> GetFilesListAsync(string containerType);
        Task<List<string>> GetAllBookNames();
        CloudBlobContainer GetBlobContainer(string containerType);
    }
}
