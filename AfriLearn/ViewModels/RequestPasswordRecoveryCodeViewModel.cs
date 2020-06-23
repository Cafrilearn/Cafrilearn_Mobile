using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.ViewModels;
using AfriLearn.Views;
using AfriLearn;
using AfriLearnMobile.Models;
using AfriLearn.Services;
using AfriLearn.Dtos;

namespace HackMenopause.ViewModels
{
    class RequestPasswordRecoveryCodeViewModel : BaseViewModel
    {
        private string email;
        public RequestPasswordRecoveryCodeViewModel()
        {
           
        }
                         
        public  string  Email
        {
            get { return  email; }
            set 
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public ICommand RequestCodeCommand => new Command(async () =>
        {
            if (Internet())
            {
                await RequestPasswordRecoveryCode();
            }
            else
            {
                await NoInternet();
            }
           
        });
        public ICommand  SignUpCommand => new Command(async () =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new SignInPage());
        });
        private async Task  RequestPasswordRecoveryCode()
        {
            try
            {
                // First check if the user exists
                var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
                if (appUser.Email != Email)
                {
                    await DisplayAlert("Error", "This email is not registered with us, please enter the correct email", "Okay");
                }
                else
                {
                    IsBusy = true;
                    var email = new   ChangePassword() { Email = Email };
                    var client = new HttpClientService();
                   // var response = await client.PostUser(email, HttpClientServiceConstants.BaseUri + "Services/ResetPassword");
                    await App.Current.MainPage.Navigation.PushAsync(new ConfirmReceivedPasswordRecoveryCodePage());
                    IsBusy = false;
                }
            }
            catch (System.Exception)
            {
                await  DisplayAlert("Error", "This email is not registered with us, please Sign Up first", "Okay");
                await App.Current.MainPage.Navigation.PushAsync(new SignUpPageOne());

            }

        }
       

    }
}
