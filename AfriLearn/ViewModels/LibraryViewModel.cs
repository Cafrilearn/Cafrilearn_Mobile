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
    class LibraryViewModel :  BaseViewModel
    {
        #region fields
        private bool headerTextVisibility = true;
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
        public List<string> SavedBooks { get; set; }
        #endregion

        public ICommand RemoveBookCommand => new Command(async() => 
        {
            var book = new Book();
            SavedBooks.Remove(book.BookName);
            await BlobCache.LocalMachine.InsertObject("savedBooks", SavedBooks);
            NavigationService.DisplayAlert("Deleted", "Book deleted, but you can always find it in explore page again", "Okay");
        });

        public async void GetSavedBooks()
        {
            try 
            { 
                SavedBooks = await BlobCache.LocalMachine.GetObject<List<string>>("savedBooks");
            }
            catch (Exception)
            {
                
            }
        }
    }
}
