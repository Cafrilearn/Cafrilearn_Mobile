using AfriLearn.Models;
using AfriLearn.Services;
using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class HistoryViewModel : BaseViewModel
    {
        #region fields
        private bool headerTextVisibility = true;
        private string bookName;
        #endregion

        public HistoryViewModel()
        {
            GetSavedBooks();
            SavedBooks = new List<Book>();
        }

        #region properties
        public List<Book> SavedBooks { get; set; }
        public bool HeaderTextVisibility
        {
            get { return headerTextVisibility; }
            set
            {
                headerTextVisibility = value;
                OnPropertyChanged(nameof(HeaderTextVisibility));
            }
        }
        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }
        #endregion

        public ICommand RemoveBookCommand => new Command(async () =>
        {
            var book = new Book();
            book.BookName = BookName;
            SavedBooks.Remove(book);
            await BlobCache.LocalMachine.Invalidate(BookName);
            await BlobCache.LocalMachine.InsertObject("savedBooks", SavedBooks);
            NavigationService.DisplayAlert("Deleted", "Book deleted, but you can always find it in explore page again", "Okay");
        });

        public ICommand RefreshListCommand => new Command(() =>
        {
            IsRefreshing = true;
            GetSavedBooks();
            IsRefreshing = false;
        });
        public async void GetSavedBooks()
        {
            try 
            { 
                var books = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
                foreach (var  book in  books)
                {
                    SavedBooks.Add(book);
                }
                SavedBooks.Reverse();
                HeaderTextVisibility = false;
            }
            catch (Exception)
            {
                
            }
        }
    }
}
