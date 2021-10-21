using AfriLearn.Constants;
using AfriLearn.Models;
using AfriLearn.Services;
using AfriLearn.Views;
using Akavache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;
using System.Text;
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
        public  ObservableCollection<Subject> Subjects { get; set; }
        #endregion

        #region methods      
       
        public void GetBookNames()
        { 
            // Have these information stored in cloud as text and retrieved when the user needs it,
            // makes the app seem smaller, and also can be modified in the backend or admin
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
                    Title = "Primary Kiswahili",
                    ImageSource = "Kiswahili.jpg",
                    NumberSymbol = "02",
                    NumberWord = "Two"
                },
                new Subject()
                {
                    Title = "Primary Mathematics",
                    ImageSource = "Mathematics.png",
                    NumberSymbol = "03",
                    NumberWord = "Three"
                },
                new Subject()
                {
                    Title = "Primary 	Physical-Education",
                    ImageSource = "PhysicalEducation.jpg",
                    NumberSymbol = "04",
                    NumberWord = "Four"
                },
                new Subject()
                {
                    Title = "Primary Religious-Education",
                    ImageSource = "ReligiousEducation.jpeg",
                    NumberSymbol = "05",
                    NumberWord = "Five"
                },
                new Subject()
                {
                    Title = "Primary Science",
                    ImageSource = "Science.png",
                    NumberSymbol = "06",
                    NumberWord = "Six"
                },
                new Subject()
                {
                    Title = "Primary Social-Studies",
                    ImageSource = "SocialStudies.jpg",
                    NumberSymbol = "07",
                    NumberWord = "Seven"
                },
                new Subject()
                {
                    Title = "Secondary Agriculture",
                    ImageSource = "Agriculture.jpg",
                    NumberSymbol = "08",
                    NumberWord = "Eight"
                },
                new Subject()
                {
                    Title = "Secondary Biology",
                    ImageSource = "Biology.jpg",
                    NumberSymbol = "09",
                    NumberWord = "Nine"
                },
                new Subject()
                {
                    Title = "Secondary Business-Studies",
                    ImageSource = "BusinessStudies.png",
                    NumberSymbol = "10",
                    NumberWord = "Ten"
                },
                new Subject()
                {
                    Title = "Secondary Chemistry",
                    ImageSource = "Chemistry.jpg",
                    NumberSymbol = "11",
                    NumberWord = "Eleven"
                },
                new Subject()
                {
                    Title = "Secondary Computer-Studies",
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
                    Title = "Secondary Geography",
                    ImageSource = "Geography.jpg",
                    NumberSymbol = "14",
                    NumberWord = "Fourteen"
                },
                new Subject()
                {
                    Title = "Secondary History",
                    ImageSource = "History.png",
                    NumberSymbol = "15",
                    NumberWord = "Fifteen"
                },
                new Subject()
                {
                    Title = "Secondary Home-Science",
                    ImageSource = "HomeScience.jpg",
                    NumberSymbol = "16",
                    NumberWord = "Sixteen"
                },
                new Subject()
                {
                    Title = "Secondary Kiswahili",
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
                    Title = "Secondary Physics",
                    ImageSource = "Physics.jpg",
                    NumberSymbol = "19",
                    NumberWord = "Ninteen"
                },
                new Subject()
                {
                    Title = "Secondary Religious-Education",
                    ImageSource = "ReligiousEducationSecondary.png",
                    NumberSymbol = "20",
                    NumberWord = "Twenty"
                },
                new Subject()
                {
                    Title = "Secondary SetBooks",
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
        public async Task GetBook(string theBookName, string level="", string subject="")
        { 
            byte[] blobBytes = null;           

            // if the book was downloaded already, just get it from blobcache and display it
            try
            {
                blobBytes = await BlobCache.LocalMachine.GetObject<byte[]>(theBookName);
            }

            //download the book, if its the first time and store the book locally as stream
            catch (Exception)
            {
                var httpClient = new HttpClientService();
                var bookBytes = await httpClient.Get(HttpClientServiceConstants.RootUri+"courses/book/"+level+"/"+subject+"/"+ theBookName);
                var bookInfo = JsonConvert.DeserializeObject<BookInfo>(bookBytes);
              //  var blobBytesString = bookInfo.book_bytes.Substring(1, bookInfo.book_bytes.Length - 2);
                blobBytes = Encoding.ASCII.GetBytes(bookInfo.book_bytes);
                await BlobCache.LocalMachine.InsertObject(theBookName, blobBytes);

                try
                {
                    var savedBooks = await BlobCache.LocalMachine.GetObject<List<Book>>("savedBooks");
                    savedBooks.Add(new Book { BookName = theBookName });
                    await BlobCache.LocalMachine.InsertObject("savedBooks", savedBooks);
                }
                catch (Exception)
                {
                    await BlobCache.LocalMachine.InsertObject("savedBooks",  new List<Book>() { new Book {BookName = theBookName } });
                }

            }

            var readshare = await Application.Current.MainPage.DisplayActionSheet("Select whether to read or share this book", "Cancel", "Okay", "Read", "Share");        
          
            if (readshare.Equals("Read"))
            {
                await BlobCache.LocalMachine.InsertObject("currentBook", theBookName);
                NavigationService.PushAsync(new ReadBookPage());
            }

            // check how this can be shared via bluetooth directly from the mobile app
            if (readshare.Equals("Share"))
            {
                var bookToSend = theBookName + ".pdf";
                var bookFile = Path.Combine(FileSystem.CacheDirectory, bookToSend);
                File.WriteAllBytes(bookFile, blobBytes);
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = Title,
                    File = new ShareFile(bookFile)
                });
            } 
        }

        #endregion
    }    
}
