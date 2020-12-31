using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.ViewModels;
using AfriLearn.Services;
using AfriLearn.Dtos;
using AfriLearn.Views;

namespace AfriLearn.ViewModels
{
    class PasswordRequstCodeViewModel : SignUpViewModel
    {
        public ICommand RequestCodeCommand => new Command(async () => await RequestPasswordRecoveryCode());
        private async Task RequestPasswordRecoveryCode()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                NavigationService.DisplayAlert("Error", "Email must must not be empty", "Okay");
                return;
            }

            if (!(Email.Contains("@outlook.com") | Email.Contains("@gmail.com")))
            {
                NavigationService.DisplayAlert("Error", "Email must contain @gmail.com or @outlook.com", "Okay");
                return;
            }

            if (!InternetService.Internet())
            {
               await InternetService.NoInternet();
            }

            IsBusy = true;

            var email = new ChangePassword() { Email = Email };
            var client = new HttpClientService();
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
