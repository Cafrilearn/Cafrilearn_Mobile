using AfriLearn.Constants;
using AfriLearn.Services;
using AfriLearn.Views;
using Akavache;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class ClassRoomViewModel : BaseViewModel
    {
        public ClassRoomViewModel()
        {

        }

        /// <summary>
        /// commands
        /// </summary>
        public ICommand ReadMathCommand => new Command(() =>
        {
            GetBookType(BookType.Mathematics);
            NavigationService.PushAsync(new ReadBookPage());
        });
        public ICommand ReadEnglishCommand => new Command(() =>
        {
             GetBookType(BookType.English);
             NavigationService.PushAsync(new ReadBookPage());
        });
        public ICommand ReadKiswahiliCommand => new Command(() =>
        {
            GetBookType(BookType.Kiswahili);
            NavigationService.PushAsync(new ReadBookPage());
        });
        public ICommand ReadScienceCommand => new Command(() =>
        {
            GetBookType(BookType.Science);
            NavigationService.PushAsync(new ReadBookPage());
        });
        public ICommand ReadSocialStudiesMathCommand => new Command(() =>
        {
            GetBookType(BookType.SocialStudies);
            NavigationService.PushAsync(new  ReadBookPage());
        });
        public ICommand ReadReligiousEducationCommand => new Command(() =>
        {
            GetBookType(BookType.ReligiousEducation);
            NavigationService.PushAsync(new ReadBookPage());
        });


        /// <summary>
        /// methods
        /// </summary>
        /// <param name="bookType"></param>
        public void GetBookType(string  bookType)
        {
            BlobCache.InMemory.InsertObject<string>("bookType",  bookType);
        }
    }
}
