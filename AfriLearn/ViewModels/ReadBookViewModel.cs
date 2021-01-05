using Akavache;
using System.IO;
using System.Reactive.Linq;

namespace AfriLearn.ViewModels
{
    class ReadBookViewModel : SubjectsViewModel
    {
        public ReadBookViewModel()
        {
            LoadCurrentBook();
        }
        
        public async void LoadCurrentBook()
        {
            BookName = await BlobCache.LocalMachine.GetObject<string>("currentBook");
            var blobBytes = await BlobCache.LocalMachine.GetObject<byte[]>(BookName);
            BookSource =  new MemoryStream(blobBytes);
        }
    }
}
