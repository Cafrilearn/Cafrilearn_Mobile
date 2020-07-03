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
        /// <summary>
        /// commands
        /// </summary>
        public ICommand ReadMathCommand => new Command(() =>
        {
            GetBookType(BookType.Mathematics);
        });
        public ICommand ReadEnglishCommand => new Command(() =>
        {
             GetBookType(BookType.English); 
        });
        public ICommand ReadKiswahiliCommand => new Command(() =>
        {
            GetBookType(BookType.Kiswahili);
        });
        public ICommand ReadScienceCommand => new Command(() =>
        {
            GetBookType(BookType.Science);
        });
        public ICommand ReadSocialStudiesMathCommand => new Command(() =>
        {
            GetBookType(BookType.SocialStudies);
        });
        public ICommand ReadReligiousEducationCommand => new Command(() =>
        {
            GetBookType(BookType.ReligiousEducation);
        });

        /// <summary>
        /// methods
        /// </summary>
        /// <param name="bookType"></param>
        public void GetBookType(string  bookType)
        {
            BlobCache.InMemory.InsertObject<string>("bookType",  bookType);
            NavigationService.PushAsync(new ReadBookPage());
        }
    }
}
