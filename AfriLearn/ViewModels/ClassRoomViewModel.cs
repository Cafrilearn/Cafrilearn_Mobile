using AfriLearn.Constants;
using AfriLearn.Dtos;
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
using AfriLearn.Models;

namespace AfriLearn.ViewModels
{
    class ClassRoomViewModel : BaseViewModel
    {
        #region fields
        private bool loadBookBlockVisibility;
        private Stream bookSource;
        private string bookName;
        #endregion

        #region commands
        public ICommand ReadMathCommand => new Command(() => GetBookStream(BookType.Mathematics));
        public ICommand ReadEnglishCommand => new Command(() => GetBookStream(BookType.English));
        public ICommand ReadKiswahiliCommand => new Command(() => GetBookStream(BookType.Kiswahili));
        public ICommand ReadScienceCommand => new Command(() => GetBookStream(BookType.Science));
        public ICommand ReadSocialStudiesCommand => new Command(() => GetBookStream(BookType.SocialStudies));
        public ICommand ReadReligiousEducationCommand => new Command(() => GetBookStream(BookType.ReligiousEducation));
        #endregion



        public Stream BookSource
        {
            get { return bookSource; }
            set
            {
                bookSource = value;
                OnPropertyChanged(nameof(BookSource));
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
        public bool LoadBookBlockVisibility
        {
            get { return loadBookBlockVisibility; }
            set
            {
                loadBookBlockVisibility = value;
                OnPropertyChanged(nameof(LoadBookBlockVisibility));
            }
        }
        
        public void GetBookStream(string bookType)
        {
            IsBusy = true;
            switch (bookType)
            {
                case BookType.Mathematics:
                    GetBook(bookType, "MATHEMATICS");
                    break;
                case BookType.English:
                    GetBook(bookType, "ENGLISH");
                    break;
                case BookType.Kiswahili:
                    GetBook(bookType, "KISWAHILI");
                    break;
                case BookType.Science:
                    GetBook(bookType, "SCIENCE");
                    break;
                case BookType.SocialStudies:
                    GetBook(bookType, "SOCIAL");
                    break;
                case BookType.ReligiousEducation:
                    GetBook(bookType, "CRE");
                    break;
                default:
                    break;
            }
            NavigationService.PushAsync(new ReadBookPage());
            IsBusy = false;
        }

        public async  void GetBook(string bookType, string bookFormat)
        {
            var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            var bookDto = new BookDto()
            {
                BookType = bookType,
                BookFormat = bookFormat
            };
            try
            {
                var allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
                var books = allBooks.Where(b => b.StartsWith(bookFormat)).ToList();
                BookName = books.Where(c => c.Contains(appUser.StudyLevel.ToUpper())).FirstOrDefault();
                BookSource = await BlobCache.LocalMachine.GetObject<Stream>(BookName);
            }
            catch (Exception)
            {
                var token = await BlobCache.UserAccount.GetObject<TokenDto>("tokenDto");
                var httpClientService = new HttpClientService(token.Token);
                var books = await httpClientService.Post(bookDto, "getallbooknames");
                bookDto = JsonConvert.DeserializeObject<BookDto>(books);
                var bookNames =  bookDto.AllBookNames.Where(b => b.StartsWith(bookFormat)).ToList();
                BookName =  bookNames.Where(c => c.Contains(appUser.StudyLevel.ToUpper())).FirstOrDefault();
                BookSource = bookDto.BookStream;
                await BlobCache.LocalMachine.InsertObject(BookName, bookDto.BookStream);
                await BlobCache.LocalMachine.InsertObject("allBookNames", bookDto.AllBookNames);
            }
        }
    }
}
 