using AfriLearn.Models;
using AfriLearn.Services;
using Akavache;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class LibraryViewModel : ExploreViewModel
    {
        /// <summary>
        /// fields
        /// </summary>
        private bool headerTextVisibility = true;
        private ObservableCollection<Book> savedBooks;

        /// <summary>
        /// constructors
        /// </summary>
        public LibraryViewModel()
        {
            GetSavedBooks();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetBooks();
        }

        /// <summary>
        /// properties
        /// </summary>
        public bool HeaderTextVisibility
        {
            get { return headerTextVisibility; }
            set
            {
                headerTextVisibility = value;
                OnPropertyChanged(nameof(HeaderTextVisibility));
            }
        }
        public  ObservableCollection<Book>  SavedBooks
        {
            get { return savedBooks; }
            set
            {
                savedBooks = value;
                OnPropertyChanged(nameof(SavedBooks));
            }
        }

        public ICommand RemoveBookCommand => new Command(async() => 
        {
            //var book = new Book() { BookTitle = SelectedBook };
            //SavedBooks.Remove(book);
            //var getSavedBookS = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
            //getSavedBookS.Remove(book);
            //await BlobCache.LocalMachine.InsertObject<List<Book>>("savedBooks", getSavedBookS);
            NavigationService.DisplayAlert("Deleted", "Book deleted, but you can always find it in explore page again", "Okay");
        });

        /// <summary>
        /// methods
        /// </summary>
        public async void GetSavedBooks()
        {
            try
            {
                var books = new  ObservableCollection<Book>();
                var getSavedBookS = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
                foreach (var book in  getSavedBookS)
                {
                    books.Add(book);
                    HeaderTextVisibility = false;
                }
                SavedBooks = books;
            }
            catch (System.Exception)
            {
                
            }
        }

    }
}
