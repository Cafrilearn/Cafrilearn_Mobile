using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.Services;
using AfriLearn.Dtos;
using AfriLearn.Views;
using AfriLearn.Helpers;
using AfriLearnMobile.Models;

namespace AfriLearn.ViewModels
{
    class PasswordRequstCodeViewModel : SignUpViewModel
    {
        public ICommand RequestCodeCommand => new Command(async () => await RequestPasswordRecoveryCode());
        private async Task RequestPasswordRecoveryCode()
        {
            if (!EmailValidatorHelper.ValidateEmail(Email))
            {
                return;
            }

            if (!InternetService.Internet())
            {
               await InternetService.NoInternet();
            }

            IsBusy = true;

           var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");

            var email = new ChangePassword() { Email = Email };
            var client = new HttpClientService(appUser.AuthKey);
            var response = await client.Post(email, "Services/ResetPassword");

            if (response == "")
            {
                NavigationService.DisplayAlert("Error", "Wrong email, please provide a valid email address", "Okay");
                IsBusy = false;
                return;
            }

            await BlobCache.InMemory.InsertObject("passwordresetcode", response);
            await BlobCache.InMemory.InsertObject("email", Email);
            NavigationService.PushAsync(new PasswordRecoveryConfirmPage());

            IsBusy = false;
        }
    }
}
