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
        #region fields
        private bool headerTextVisibility = true;
        private  List<Book> savedBooks;
        #endregion
        public LibraryViewModel()
        {
            GetSavedBooks();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetBooks();
        }

        #region properties
        public bool HeaderTextVisibility
        {
            get { return headerTextVisibility; }
            set
            {
                headerTextVisibility = value;
                OnPropertyChanged(nameof(HeaderTextVisibility));
            }
        }
        public  List<Book>  SavedBooks
        {
            get { return savedBooks; }
            set
            {
                savedBooks = value;
                OnPropertyChanged(nameof(SavedBooks));
            }
        }
        #endregion

        public ICommand RemoveBookCommand => new Command(async() => 
        {
            var book = new Book();
            SavedBooks.Remove(book);
            var getSavedBookS = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
            getSavedBookS.Remove(book);
            await BlobCache.LocalMachine.InsertObject("savedBooks", getSavedBookS);
            NavigationService.DisplayAlert("Deleted", "Book deleted, but you can always find it in explore page again", "Okay");
        });

        public async void GetSavedBooks()
        {
            try
            {
                var books = new   List<Book>();
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
