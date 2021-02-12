using AfriLearn.Helpers;
using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using Newtonsoft.Json;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SignInViewModel :  SignUpViewModel
    {
        public ICommand NavigateToSignUpPageCommand => new Command(() =>
        {
            NavigationService.PushAsync(new SignUpPage());
        });

        public ICommand NavigateToRequestPasswordRecoveryCodePage =>  new Command(() => NavigationService.PushAsync(new PasswordRequstCodePage()));
        public ICommand SignInCommand => new Command(() => SignIn());
        
        private async void SignIn()
        {

            if (!EmailValidatorHelper.ValidateEmail(Email))
            {
                return;
            }

            if (!PasswordValidator.ValidatePassword(Password, ConfirmPassword))
            {
                return;
            }


            if (!InternetService.Internet())
            {
                await InternetService.NoInternet();
                return;
            }

            IsBusy = true;
            AccountBlockVisibility = false;

            var authCred = new AuthDto()
            {
                Email = this.Email.ToLower(),
                Password = this.Password
            };

            var httpClient = new HttpClientService();
            var response = await httpClient.Post(authCred, "User/authenticateUser");


            if (response == "Username or Passsword not correct" || response == "")
            {
                var wrongPassword = await NavigationService.DisplayAlert("Error", "Wrong Password or Email", "Try Again", "Reset Password");
                if (!wrongPassword)
                {
                    NavigationService.PushAsync(new PasswordRequstCodePage());
                }
                IsBusy = false;
                AccountBlockVisibility = true;
                return;
            }

            var appUserAccount = JsonConvert.DeserializeObject<AppUser>(response);
            appUserAccount.IsSignedIn = true;
            await BlobCache.UserAccount.InsertObject("appUser", appUserAccount);
            NavigationService.PushAsync(new HomePage());

            IsBusy = false;
            AccountBlockVisibility = true;
        }
    }
    
}
