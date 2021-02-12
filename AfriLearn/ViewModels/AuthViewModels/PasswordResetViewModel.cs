using System.Windows.Input;
using Xamarin.Forms;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.ViewModels;
using AfriLearn.Services;
using AfriLearnMobile.Models;
using AfriLearn.Views;
using Newtonsoft.Json;
using AfriLearn.Helpers;

namespace AfriLearnMobile.ViewModels
{
    class PasswordResetViewModel :  SignUpViewModel
    {
        #region commands
        public ICommand SetNewPasswordComand => new Command(() => SetNewPassword());
        public ICommand SignUpCommnd => new Command(() => NavigationService.PushAsync(new SignUpPage()));
        #endregion

        private async void SetNewPassword()
        {
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

            var email = await BlobCache.InMemory.GetObject<string>("email");

            var authDto = new AuthDto()
            {
                Email = email,
                Password = Password
            };

            var authHttpClient = new HttpClientService();
            var passResetResponse = await authHttpClient.Post(authDto, "User/passwordreset");
            var currentUserInfor = JsonConvert.DeserializeObject<AppUser>(passResetResponse);
            await BlobCache.UserAccount.InsertObject("appUser", currentUserInfor);
            NavigationService.PushAsync(new  LandingPage());

            IsBusy = false;
        }
    }
}
