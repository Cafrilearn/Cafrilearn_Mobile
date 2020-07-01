using AfriLearn.Dtos;
using AfriLearn.Services;
using AfriLearnMobile.Models;
using Akavache;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AfriLearn.ViewModels
{
    class ReadBookViewModel : BaseViewModel
    {
        private Stream bookSource;
        private string bookName;

        public ReadBookViewModel()
        {
            GetBookStream();
        }
                
        public  Stream  BookSource
        {
            get { return   bookSource; }
            set
            {
                bookSource = value;
                OnPropertyChanged(nameof(BookSource));
            }
        }
        public  string  BookName
        {
            get { return  bookName; }
            set 
            {
                bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }


        public void GetBookStream()
        {
               var bookType = BlobCache.LocalMachine.GetObject<string>("bookToRead");
            // List<string> allBooks = (List<string>)BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
            // AppUser appUser = (AppUser)BlobCache.UserAccount.GetObject<AppUser>("appUser");
            string bookName = null;
            if (bookType.ToString() == "mathematics")
            {
                //bookName = allBooks.Where(b => b.StartsWith("Mathematics".ToUpper())).LastOrDefault();
                BookName = bookName;
                string demoName = "MATHEMATICS REVISION NOTES STD 7 & 8-1.pdf";
                GetBookFromAzure("mathematics",  demoName);
            }
            else if (bookType.ToString() == "english")
            {
               // bookName = allBooks.Where(b => b.StartsWith("ENGLISH".ToUpper())).LastOrDefault();
                BookName = bookName;
                string demoName = "ENGLISH GRAMMAR.pdf";
                GetBookFromAzure(bookType.ToString(),  demoName);
            }
            else if (bookType.ToString() == "kiswahili")
            {
              //  bookName = allBooks.Where(b => b.StartsWith("KISWAHILI".ToUpper())).FirstOrDefault();
                BookName = bookName;
                string demoName = "KISWAHILI DARASA LA 4.pdf";
                GetBookFromAzure(bookType.ToString(),  demoName);
            }
            else if (bookType.ToString() == "science")
            {
               // bookName = allBooks.Where(b => b.StartsWith("Science".ToUpper())).LastOrDefault();
                BookName = bookName;
                string demoName = "SCIENCE CLASS 6 NOTES.pdf";
                GetBookFromAzure(bookType.ToString(),  demoName);
            }
            else if (bookType.ToString() == "social-studies")
            {
                //bookName = allBooks.Where(b => b.StartsWith("SOCIAL".ToUpper())).LastOrDefault();
                BookName = bookName;
                string demoName = "SOCIAL CLASS 6 NOTES.pdf";
                GetBookFromAzure(bookType.ToString(), demoName);
            }
            else if (bookType.ToString() == "physical-education")
            {
                //bookName = allBooks.Where(b => b.StartsWith("PE".ToUpper())).LastOrDefault();
                BookName = bookName;
                string demoName = "PE CHILD RIGHTS.pdf";
                GetBookFromAzure(bookType.ToString(),  demoName);
            }
           
        }

        public void GetBookFromAzure(string bookType,   string bookName)
        {
            BookSource = AzureBlobStorageService
                               .GetBlobAsync(bookType, bookName).Result;
        }
    }
}
