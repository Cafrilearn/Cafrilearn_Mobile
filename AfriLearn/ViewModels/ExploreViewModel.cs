using AfriLearn.Constants;
using AfriLearn.Services;
using AfriLearnMobile.Models;
using Akavache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace AfriLearn.ViewModels
{
    class ExploreViewModel :  ClassRoomViewModel
    {
      
        private  List<string> allAfriLearnBooks;
        public ExploreViewModel()
        {
          GetBooks();
        }        

        #region methods
        public List<string> AllAfriLearnBooks
        {
            get { return  allAfriLearnBooks; }
            set
            {
                allAfriLearnBooks = value;
                OnPropertyChanged(nameof(AllAfriLearnBooks));
            }
        }
        public async  void GetBooks()
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
        public static void BookSelected(string bookSelected)
        {
            var cvm = new ClassRoomViewModel();
            if (bookSelected.Contains("MATHEMATICS"))
            {
               cvm.GetBook(BookType.Mathematics);
            }
            else if (bookSelected.Contains("ENGLISH"))
            {
               cvm.GetBook(BookType.English); 
            }
            else if (bookSelected.Contains("KISWAHILI"))
            {
               cvm.GetBook(BookType.Kiswahili);
            }
            else if (bookSelected.Contains("SCIENCE"))
            {
               cvm.GetBook(BookType.Science);
            }
            else if (bookSelected.Contains("SOCIAL"))
            {
               cvm.GetBook(BookType.SocialStudies);
            }
            else if (bookSelected.Contains("CRE"))
            {
                cvm.GetBook(BookType.ReligiousEducation);
            }
        }
        #endregion
    }
}
