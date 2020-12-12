﻿using AfriLearn.Constants;
using AfriLearn.Models;
using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace AfriLearn.ViewModels
{
    class ClassRoomViewModel : BaseViewModel
    {
        #region fields
        private Stream bookSource;
        private string bookName;
        private bool mainContentVisibility = true;
        private byte [] bookBytes;
        #endregion

        #region commands
        public ICommand ReadMathCommand => new Command(async () => await GetBook(BookType.Mathematics));
        public ICommand ReadEnglishCommand => new Command(async () => await GetBook(BookType.English));
        public ICommand ReadKiswahiliCommand => new Command(async () => await GetBook(BookType.Kiswahili));
        public ICommand ReadScienceCommand => new Command(async () => await GetBook(BookType.Science));
        public ICommand ReadSocialStudiesCommand => new Command(async () => await GetBook(BookType.SocialStudies));
        public ICommand ReadReligiousEducationCommand => new Command(async () => await GetBook(BookType.ReligiousEducation));
        public ICommand GoToExplorePage => new Command(() => NavigationService.PushAsync(new ExplorePage()));
        #endregion

        #region properties
        public Stream BookSource
        {
            get { return bookSource; }
            set
            {
                bookSource = value;
                OnPropertyChanged(nameof(BookSource));
            }
        }
        public byte [] BookBytes
        {
            get { return  bookBytes; }
            set 
            {
                bookBytes = value;
                OnPropertyChanged(nameof(BookBytes));
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
        public  bool MainContentVisibility
        {
            get { return  mainContentVisibility; }
            set 
            {
                mainContentVisibility = value;
                OnPropertyChanged(nameof(MainContentVisibility));
            }
        }
        #endregion

        public async  Task GetBook(string bookFormat)
        {
            IsBusy = true;
            MainContentVisibility = false ;

            byte[] blobBytes = null;
            List<string> books;

            var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            var httpClientService = new HttpClientService(appUser.AuthKey);
            var book = new Book() 
            { 
                ContainerType = bookFormat
            };          

            // come back to try and download the book after getting the book names
            getbookagain:
            try
            {
                var allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
                books = allBooks.Where(b => b.Contains(bookFormat)).ToList();
                var relativeBookPath = books.Where(c => c.Contains(appUser.StudyLevel.ToUpper())).FirstOrDefault();
                var absPathStart = relativeBookPath.LastIndexOf('/');
                BookName = relativeBookPath.Substring(absPathStart + 1);
              
                // if the book was downloaded already, just get it from blobcache and display it
                try
                {
                    blobBytes = await BlobCache.LocalMachine.GetObject<byte[]>(BookName);
                    BookBytes = blobBytes;
                }

                //download the book, if its the first time and store the book locally as stream
                catch (Exception)
                {
                    book.BookName =  BookName;
                    blobBytes = await MediaService.GetBlobAsync(book);
                    BookBytes = blobBytes;
                    await BlobCache.LocalMachine.InsertObject(BookName, blobBytes);
                    try
                    {
                        var savedBooks = await BlobCache.LocalMachine.GetObject<ObservableCollection<Book>>("savedBooks");
                        savedBooks.Add(book);
                        await BlobCache.LocalMachine.InsertObject("savedBooks", savedBooks); 
                    }
                    catch (Exception)
                    {
                        var newBook = new List<Book>() { book };
                        await BlobCache.LocalMachine.InsertObject("savedBooks", newBook);
                    }
                }



            }
            // do this to download and store all book names locally
            catch (Exception)
            {
                var allBooksResponse = await httpClientService.Get("Books/getallbooknames");
                var allBookNames = JsonConvert.DeserializeObject<List<string>>(allBooksResponse);
                await BlobCache.LocalMachine.InsertObject("allBookNames", allBookNames);
                goto getbookagain;
            }

            var readshare = await Application.Current.MainPage.DisplayActionSheet("Select whether to read or share book","Cancel", "Okay", "Read", "Share");
            
            if (readshare.Equals("Read"))
            {
                await BlobCache.LocalMachine.InsertObject("currentBook", BookName);
                NavigationService.PushAsync(new ReadBookPage());
            }

            if (readshare.Equals("Share"))
            {
                var content = Convert.ToBase64String(BookBytes);
                /*
                await Share.RequestAsync(new ShareTextRequest() 
                {
                    Title = BookName,
                    Text = content
                });     */

                var testBook = "testBook.txt";
                var bookFile = Path.Combine(FileSystem.CacheDirectory, testBook);
                File.WriteAllText(bookFile, content);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = Title,
                    File = new ShareFile(bookFile)
                });
            }         

            IsBusy = false;
            MainContentVisibility = true;
        }        
    }
}
 