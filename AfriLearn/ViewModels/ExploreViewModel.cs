using AfriLearn.Constants;
using AfriLearn.Models;
using AfriLearn.Services;
using Akavache;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class ExploreViewModel :  ClassRoomViewModel
    {
        /// <summary>
        /// fields
        /// </summary>
        private string selectedBook;

        /// <summary>
        /// ctr
        /// </summary>
        public ExploreViewModel()
        {
          Books = GetBooks().Result;
        }

        /// <summary>
        /// properties
        /// </summary>
        public  List<Book> Books { get; set; }
        public  string  SelectedBook
        {
            get { return  selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        /// <summary>
        /// commands
        /// </summary>
        public ICommand  BookSelectedCommand => new Command(() =>
        {
            if (SelectedBook.StartsWith("MATHEMATICS"))
            {
                GetBookType(BookType.Mathematics);
            }
            else if (SelectedBook.StartsWith("ENGLISH"))
            {
                GetBookType(BookType.English); ;
            }
            else if (SelectedBook.StartsWith("KISWAHILI"))
            {
                GetBookType(BookType.Kiswahili);
            }
            else if (SelectedBook.StartsWith("SCIENCE"))
            {
                GetBookType(BookType.Science);
            }
            else if (SelectedBook.StartsWith("SOCIAL"))
            {
                GetBookType(BookType.SocialStudies);
            }
            else if (SelectedBook.StartsWith("CRE"))
            {
                GetBookType(BookType.ReligiousEducation);
            }
        });
        public ICommand AddBookToLibraryCommand => new Command(async () =>
        {
            var book = new Book() { BookTitle = SelectedBook };
            try
            {
                var savedBooks = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
                savedBooks.Add(book);
                await BlobCache.LocalMachine.InsertObject<List<Book>>("savedBooks", savedBooks);
                NavigationService.DisplayAlert("Book Saved", "Book added to library, keep learning!", "Okay");
            }
            catch (System.Exception)
            {
                var  newBook = new List<Book>() { book };
                await BlobCache.LocalMachine.InsertObject<List<Book>>("savedBooks", newBook);
                NavigationService.DisplayAlert("Success", "Your first Book has been added to library, keep learning!", "Okay");
            }
        });
        /// <summary>
        /// methods
        /// </summary>
        public async Task<List<Book>> GetBooks()
        {
            //var books = new List<Book>();
            //var allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
            //foreach (var book in allBooks)
            //{
            //    books.Add(new Book() { BookTitle = book });
            //}
            return null;
        }

    }
}
