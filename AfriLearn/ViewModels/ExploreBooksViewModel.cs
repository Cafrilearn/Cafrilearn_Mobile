using AfriLearn.Constants;
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
    class ExploreBooksViewModel : BaseViewModel
    {
        #region fields
        private Stream bookSource;
        private string bookName;
        private bool mainContentVisibility = true;
        private byte[] bookBytes;
        #endregion
        public ExploreBooksViewModel()
        {
            // get the books once, assign them to AllAfriLearnBooks then filter 
            // by subject late to a assign to each subject list
            GetBookNames();
            GetSubjects();
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
        #endregion

        #region methods
        public List<string> AllAfriLearnBooks { get;  set;}
        public List<Subject> Subjects { get; set; }

        public async Task GetBook(string bookFormat)
        {
            IsBusy = true;
            MainContentVisibility = false;

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
                var allBooksResponse = await httpClientService.Get("Books/getallbooknames");
                var allBookNames = JsonConvert.DeserializeObject<List<string>>(allBooksResponse);
                await BlobCache.LocalMachine.InsertObject("allBookNames", allBookNames);
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
        public async  void GetBookNames()
        {
            try
            {                
                var allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
                foreach (var book in allBooks)
                {
                    AllAfriLearnBooks.Add(book);
                }
            }
            catch (Exception)
            {
                var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
                var httpClientService = new HttpClientService(appUser.AuthKey);
                var books = await httpClientService.Get("Books/getallbooknames");
                var booksList = JsonConvert.DeserializeObject<List<string>>(books);
                
                AllAfriLearnBooks = booksList;
                await BlobCache.LocalMachine.InsertObject("allBookNames", booksList);
            }
        }
        public void GetSubjects()
        {
            Subjects = new List<Subject>()
            {
                new Subject()
                {
                    Title = "Primary English", 
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.English)).ToList()
                },
                new Subject()
                {
                    Title = "Primary  Kiswahili",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Kiswahili)).ToList()
                },
                new Subject()
                {
                    Title = "Primary  Mathematics",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Mathematics)).ToList()
                },               
                new Subject()
                {
                    Title = "Physical  Education",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.PhysicalEducation)).ToList()
                },
                new Subject()
                {
                    Title = "Primary  Religious Education",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.ReligiousEducation)).ToList()
                },
                new Subject()
                {
                    Title = "Science",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Science)).ToList()
                },
                new Subject()
                {
                    Title = "Social Studies",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.SocialStudies)).ToList()
                } ,
                new Subject()
                {
                    Title = "Agriculture",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Agriculture)).ToList()
                },
                new Subject()
                {
                    Title = "Biology",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Biology)).ToList()
                },
                new Subject()
                {
                    Title = "Business Studies",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.BusinessStudies)).ToList()
                },
                new Subject()
                {
                    Title = "Chemistry",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Chemistry)).ToList()
                },
                new Subject()
                {
                    Title = "Computer Studies",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.ComputerStudies)).ToList()
                },               
                new Subject()
                {
                    Title = "Secondary English",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.SecEnglish)).ToList()
                },
                new Subject()
                {
                    Title = "Geography",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Geography)).ToList()
                },
                new Subject()
                {
                    Title = "History",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.History)).ToList()
                },
                new Subject()
                {
                    Title = "Home Science",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.HomeScience)).ToList()
                },
                new Subject()
                {
                    Title = "Secondary  Kiswahili",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.SecKiswahili)).ToList()
                },
                new Subject()
                {
                    Title = "Mathematics",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.SecMathematics)).ToList()
                },
                new Subject()
                {
                    Title = "Physics",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Physics)).ToList()
                },
                new Subject()
                {
                    Title = "Secondary Religious Education",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.SecReligiousEducation)).ToList()
                },
                new Subject()
                {
                    Title = "Set  Books",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.SetBooks)).ToList()
                },
                new Subject()
                {
                    Title = "Explore More",
                    ImageSource = "English.jpg",
                    Books = AllAfriLearnBooks.Where(b => b.Contains(BookType.Others)).ToList()
                }
            };
        }
        public async void BookSelected(string bookSelected)
        {
            if (bookSelected.Contains("MATHEMATICS"))
            {
               await GetBook(BookType.Mathematics);
            }
            else if (bookSelected.Contains("ENGLISH"))
            {
               await GetBook(BookType.English); 
            }
            else if (bookSelected.Contains("KISWAHILI"))
            {
               await GetBook(BookType.Kiswahili);
            }
            else if (bookSelected.Contains("SCIENCE"))
            {
               await GetBook(BookType.Science);
            }
            else if (bookSelected.Contains("SOCIAL"))
            {
               await GetBook(BookType.SocialStudies);
            }
            else if (bookSelected.Contains("CRE"))
            {
               await GetBook(BookType.ReligiousEducation);
            }
        }
        #endregion
    }
}
