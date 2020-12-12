using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using AfriLearn.Services;
using AfriLearn.Dtos;

namespace AfriLearn.ViewModels
{
    class RequestPasswordRecoveryCodeViewModel : SignUpViewModel
    {
       
        public RequestPasswordRecoveryCodeViewModel()
        {
           
        }
                         
        
        public ICommand RequestCodeCommand => new Command(async () =>
        {
            if (InternetService.Internet())
            {
                 await RequestPasswordRecoveryCode();
            }
            else
            {
                await InternetService.NoInternet();
            }
           
        });
        private async Task  RequestPasswordRecoveryCode()
        {
            try
            {
                // First check if the user exists
                var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
                if (appUser.Email != Email)
                {
                    NavigationService.DisplayAlert("Error", "This email is not registered with us, please enter the correct email", "Okay");
                }
                else
                {
                    IsBusy = true;
                    var email = new ChangePassword() { Email = Email };
                    var client = new HttpClientService();
                    // var response = await client.PostUser(email, HttpClientServiceConstants.BaseUri + "Services/ResetPassword");

                    //start for demo
                    await Task.Delay(4000);
                    NavigationService.PushAsync(new PasswordRecoveryConfirmPage());
                    // end for demo                   
                    IsBusy = false;
                }
            }
            catch (System.Exception)
            {
                 NavigationService.DisplayAlert("Error", "This email is not registered with us, please Sign Up first", "Okay");
            }
        }  
    }
}
