using AfriLearn.Constants;
using AfriLearn.Models;
using AfriLearn.Services;
using AfriLearnMobile.Models;
using Akavache;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class ReadBookViewModel : BaseViewModel
    {
        /// <summary>
        /// fields
        /// </summary>
        private Stream bookSource;
        private string bookName;
        private bool loadBookBlockVisibility;

        /// <summary>
        /// constructor(s)
        /// </summary>
        public ReadBookViewModel()
        {
            GetBookStream();
        }
             
        /// <summary>
        /// properties
        /// </summary>
        public  Stream  BookSource
        {
            get { return   bookSource; }
            set
            {
                bookSource = value;
                OnPropertyChanged(nameof(BookSource));
            }
        }
        public  string  BookName
        {
            get { return  bookName; }
            set 
            {
                bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }
        public  bool LoadBookBlockVisibility
        {
            get { return  loadBookBlockVisibility; }
            set 
            {
                loadBookBlockVisibility = value;
                OnPropertyChanged(nameof(LoadBookBlockVisibility));
            }
        }


        public ICommand SaveBookToLibraryCommand => new Command(() => 
        {
            AddBookToLibrary();
        });

        /// <summary>
        /// methods
        /// </summary>
        public async void GetBookStream()
        {
            IsBusy = true;
            var bookType = await BlobCache.InMemory.GetObject<string>("bookType");
            switch (bookType)
            {
                case BookType.Mathematics:
                    GetBookFromAzure(bookType, "MATHEMATICS");
                    break;
                case BookType.English:
                    GetBookFromAzure(bookType, "ENGLISH");
                    break;
                case BookType.Kiswahili:
                    GetBookFromAzure(bookType, "KISWAHILI");
                    break;
                case BookType.Science:
                    GetBookFromAzure(bookType, "SCIENCE");
                    break;
                case BookType.SocialStudies:
                    GetBookFromAzure(bookType, "SOCIAL");
                    break;
                case BookType.ReligiousEducation:
                    GetBookFromAzure(bookType, "CRE");
                    break;
                default:
                    break;
            }
            IsBusy = false;
        }

        public async void GetBookFromAzure(string bookType, string bookFormat)
        {
            var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            var allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
            var books = allBooks.Where(b => b.StartsWith(bookFormat)).ToList();
            BookName = books.Where(c => c.Contains(appUser.StudyLevel.ToUpper())).FirstOrDefault();

            try
            {
                BookSource = await BlobCache.LocalMachine.GetObject<Stream>(BookName);
            }
            catch (System.Exception)
            {

                BookSource = AzureBlobStorageService
                               .GetBlobAsync(bookType, BookName).Result;
            }
            
        }
        public async void AddBookToLibrary()
        {
            var book = new Book() { BookTitle = BookName };
            try
            {
                var savedBooks = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
                savedBooks.Add(book);
                await BlobCache.LocalMachine.InsertObject<List<Book>>("savedBooks", savedBooks);
                NavigationService.DisplayAlert("Book Saved", "Book added to library, keep learning!", "Okay");
            }
            catch (System.Exception)
            {
                var newBook = new List<Book>() { book };
                await BlobCache.LocalMachine.InsertObject<List<Book>>("savedBooks", newBook);
                NavigationService.DisplayAlert("Success", "Your first Book has been added to library, keep learning!", "Okay");
            }
        }

    }
}
