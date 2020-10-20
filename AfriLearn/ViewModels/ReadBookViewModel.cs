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
    class ReadBookViewModel : ClassRoomViewModel
    {  
        public ICommand SaveBookToLibraryCommand => new Command(() => AddBookToLibrary());
        public async void AddBookToLibrary()
        {
            var book = new Book() { BookTitle = BookName };
            try
            {
                var savedBooks = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
                savedBooks.Add(book);
                await BlobCache.LocalMachine.InsertObject("savedBooks", savedBooks);
                NavigationService.DisplayAlert("Book Saved", "Book added to library, keep learning!", "Okay");
            }
            catch (Exception)
            {
                var newBook = new List<Book>() { book };
                await BlobCache.LocalMachine.InsertObject("savedBooks", newBook);
                NavigationService.DisplayAlert("Success", "Your first Book has been added to library, keep learning!", "Okay");
            }
        }
    }
}
