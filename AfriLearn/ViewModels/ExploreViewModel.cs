using AfriLearn.Constants;
using AfriLearn.Models;
using Akavache;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace AfriLearn.ViewModels
{
    class ExploreViewModel :  ClassRoomViewModel
    {
        /// <summary>
        /// fields
        /// </summary>
        private ObservableCollection<Book> allAfriLearnBooks;

        /// <summary>
        /// ctr
        /// </summary>
        public ExploreViewModel()
        {
          GetBooks();
        }

        /// <summary>
        /// properties
        /// </summary>
        public ObservableCollection<Book> AllAfriLearnBooks
        {
            get { return  allAfriLearnBooks; }
            set
            {
                allAfriLearnBooks = value;
                OnPropertyChanged(nameof(AllAfriLearnBooks));
            }
        }

        /// <summary>
        /// methods
        /// </summary>
        public async  void GetBooks()
        {
            try
            {
                var books = new  ObservableCollection<Book>();
                var allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
                foreach (var book in allBooks)
                {
                  books.Add(new Book() { BookTitle = book });
                }
                AllAfriLearnBooks = books;
            }
            catch (System.Exception)
            {

            }
        }
        public static void BookSelected(string bookSelected)
        {
            if (bookSelected.StartsWith("MATHEMATICS"))
            {
                GetBookType(BookType.Mathematics);
            }
            else if (bookSelected.StartsWith("ENGLISH"))
            {
                GetBookType(BookType.English); ;
            }
            else if (bookSelected.StartsWith("KISWAHILI"))
            {
                GetBookType(BookType.Kiswahili);
            }
            else if (bookSelected.StartsWith("SCIENCE"))
            {
                GetBookType(BookType.Science);
            }
            else if (bookSelected.StartsWith("SOCIAL"))
            {
                GetBookType(BookType.SocialStudies);
            }
            else if (bookSelected.StartsWith("CRE"))
            {
                GetBookType(BookType.ReligiousEducation);
            }
        }

    }
}
