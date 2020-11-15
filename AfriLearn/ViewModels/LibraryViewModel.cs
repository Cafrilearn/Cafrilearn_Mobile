using AfriLearn.Models;
using AfriLearn.Services;
using Akavache;
using System;
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
        private ObservableCollection<Book> savedBooks;
        #endregion
        public LibraryViewModel()
        {
            GetSavedBooks();
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
        public  ObservableCollection<Book>  SavedBooks
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
            await BlobCache.LocalMachine.InsertObject("savedBooks", SavedBooks);
            NavigationService.DisplayAlert("Deleted", "Book deleted, but you can always find it in explore page again", "Okay");
        });

        public async void GetSavedBooks()
        {
            try
            {
                var books = new ObservableCollection<Book>();
                var savedBookS = await BlobCache.LocalMachine.GetObject<ObservableCollection<Book>>("savedBooks");
                foreach (var book in  savedBookS)
                {
                    books.Add(book);
                    HeaderTextVisibility = false;
                }
                SavedBooks = books;
            }
            catch (Exception)
            {
                
            }
        }
    }
}
