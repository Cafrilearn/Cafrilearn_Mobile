using AfriLearn.Dtos;
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
        public ICommand ReadMathCommand => new Command(() =>
        {
            GetBookType("mathematics");
        });
        public ICommand ReadEnglishCommand => new Command(async () =>
        {
            //GetBookType("english");
            await App.Current.MainPage.Navigation.PushAsync(new ReadBookTest());
        });
        public ICommand ReadKiswahiliCommand => new Command(() =>
        {
            GetBookType("kiswahili");
        });
        public ICommand ReadScienceCommand => new Command(() =>
        {
            GetBookType("science");
        });
        public ICommand ReadSocialStudiesMathCommand => new Command(() =>
        {
             GetBookType("social-studies");
        });
        public ICommand ReadPECommand => new Command(() =>
        {
            GetBookType("physical-education");
        });

        public async void GetBookType(string bookName)
        {
            var bookToRead = new BookToReadDto();
            bookToRead.BookTitle = bookName;
            try
            {
                BlobCache.LocalMachine.InvalidateObject<string>("bookToRead");
                BlobCache.LocalMachine.InsertObject<string>("bookToRead", bookName);
                await App.Current.MainPage.Navigation.PushAsync(new ReadBookPage());
            }
            catch (System.Exception)
            {

                BlobCache.LocalMachine.InsertObject<string>("bookToRead",  bookName);
                await App.Current.MainPage.Navigation.PushAsync(new ReadBookPage());
            }
        }
    }
}
