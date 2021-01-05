﻿using AfriLearn.Constants;
using AfriLearn.Models;
using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SubjectsViewModel : BaseViewModel
    {
        #region fields
        private Stream bookSource;
        private string bookName;
        private bool mainContentVisibility = true;
        private byte[] bookBytes;
        #endregion
        public SubjectsViewModel()
        {
           GetBookNames();
        }

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
        public byte[] BookBytes
        {
            get { return bookBytes; }
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
        public bool MainContentVisibility
        {
            get { return mainContentVisibility; }
            set
            {
                mainContentVisibility = value;
                OnPropertyChanged(nameof(MainContentVisibility));
            }
        }
        public  ObservableCollection<Subject> Subjects { get; set; }
        #endregion

        #region methods


        public async Task GetBook(string bookFormat, string bookName)
        {
            IsBusy = true;
            MainContentVisibility = false; // do this for selectedbook page.

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
                    book.BookName = BookName;
                    blobBytes = await MediaService.GetBlobAsync(book);
                    BookBytes = blobBytes;

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

                    await BlobCache.LocalMachine.InsertObject(BookName, blobBytes);
                    await BlobCache.LocalMachine.InsertObject("currentBook", BookName);
                }
            }
            // do this to download and store all book names locally
            catch (Exception)
            {
               
                goto getbookagain;
            }

            var readshare = await Application.Current.MainPage.DisplayActionSheet("Select whether to read or share this book", "Cancel", "Okay", "Read", "Share");

            if (readshare.Equals("Read"))
            {
                await BlobCache.LocalMachine.InsertObject("currentBook", BookName);
                NavigationService.PushAsync(new ReadBookPage());
            }

            if (readshare.Equals("Share"))
            {             
                var  bookToSend = "book.pdf";
                var bookFile = Path.Combine(FileSystem.CacheDirectory, bookToSend);
                File.WriteAllBytes(bookFile,  blobBytes);
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = Title,
                    File = new ShareFile(bookFile)
                });
            }

            IsBusy = false;
            MainContentVisibility = true;
        }
      
       
        public void GetBookNames()
        {          
            Subjects = new  ObservableCollection<Subject>()
            {
                new Subject()
                {
                    Title = "Primary English",
                    ImageSource = "English.jpg",
                    NumberSymbol = "01",
                    NumberWord = "One"
                },
                new Subject()
                {
                    Title = "Primary  Kiswahili",
                    ImageSource = "Kiswahili.jpg",
                    NumberSymbol = "02",
                    NumberWord = "Two"
                },
                new Subject()
                {
                    Title = "Primary  Mathematics",
                    ImageSource = "Mathematics.png",
                    NumberSymbol = "03",
                    NumberWord = "Three"
                },
                new Subject()
                {
                    Title = "Physical  Education",
                    ImageSource = "PhysicalEducation.jpg",
                    NumberSymbol = "04",
                    NumberWord = "Four"
                },
                new Subject()
                {
                    Title = "Primary  Religious Education",
                    ImageSource = "ReligiousEducation.jpeg",
                    NumberSymbol = "05",
                    NumberWord = "Five"
                },
                new Subject()
                {
                    Title = "Science",
                    ImageSource = "Science.png",
                    NumberSymbol = "06",
                    NumberWord = "Six"
                },
                new Subject()
                {
                    Title = "Social Studies",
                    ImageSource = "SocialStudies.jpg",
                    NumberSymbol = "07",
                    NumberWord = "Seven"
                },
                new Subject()
                {
                    Title = "Agriculture",
                    ImageSource = "Agriculture.jpg",
                    NumberSymbol = "08",
                    NumberWord = "Eight"
                },
                new Subject()
                {
                    Title = "Biology",
                    ImageSource = "Biology.jpg",
                    NumberSymbol = "09",
                    NumberWord = "Nine"
                },
                new Subject()
                {
                    Title = "Business Studies",
                    ImageSource = "BusinessStudies.png",
                    NumberSymbol = "10",
                    NumberWord = "Ten"
                },
                new Subject()
                {
                    Title = "Chemistry",
                    ImageSource = "Chemistry.jpg",
                    NumberSymbol = "11",
                    NumberWord = "Eleven"
                },
                new Subject()
                {
                    Title = "Computer Studies",
                    ImageSource = "ComputerStudies.png",
                    NumberSymbol = "12",
                    NumberWord = "Twelve"
                },
                new Subject()
                {
                    Title = "Secondary English",
                    ImageSource = "SecondaryEnglish.jpg",
                    NumberSymbol = "13",
                    NumberWord = "Thirteen"
                },
                new Subject()
                {
                    Title = "Geography",
                    ImageSource = "Geography.jpg",
                    NumberSymbol = "14",
                    NumberWord = "Fourteen"
                },
                new Subject()
                {
                    Title = "History",
                    ImageSource = "History.png",
                    NumberSymbol = "15",
                    NumberWord = "Fifteen"
                },
                new Subject()
                {
                    Title = "Home Science",
                    ImageSource = "HomeScience.jpg",
                    NumberSymbol = "16",
                    NumberWord = "Sixteen"
                },
                new Subject()
                {
                    Title = "Secondary  Kiswahili",
                    ImageSource = "KiswahiliSecondary.jpg",
                    NumberSymbol = "17",
                    NumberWord = "Seventeen"
                },
                new Subject()
                {
                    Title = "Seconadry Mathematics",
                    ImageSource = "SecondaryMathematics.jpg",
                    NumberSymbol = "18",
                    NumberWord = "Eighteen"
                },
                new Subject()
                {
                    Title = "Physics",
                    ImageSource = "Physics.jpg",
                    NumberSymbol = "19",
                    NumberWord = "Ninteen"
                },
                new Subject()
                {
                    Title = "Secondary Religious Education",
                    ImageSource = "ReligiousEducationSecondary.png",
                    NumberSymbol = "20",
                    NumberWord = "Twenty"
                },
                new Subject()
                {
                    Title = "Set  Books",
                    ImageSource = "SetBooks.jpg",
                    NumberSymbol = "21",
                    NumberWord = "Twenty one"
                },
                new Subject()
                {
                    Title = "Explore More",
                    ImageSource = "Explorer.png",
                    NumberSymbol = "22",
                    NumberWord = "Twenty two"
                }
            };
        }        
        #endregion
    }    
}