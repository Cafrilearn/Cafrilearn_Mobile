using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SignInViewModel :  SignUpViewModel
    {
        /// <summary>
        /// constructors
        /// </summary>
        public SignInViewModel()
        {

        }

        /// <summary>
        /// commands
        /// </summary>
        public ICommand NavigateToSignUpPageCommand =>
           new Command(() => NavigationService.PushAsync(new  SignUpPage()));
        public ICommand NavigateToRequestPasswordRecoveryCodePage =>
          new Command(() => NavigationService.PushAsync(new RequestPasswordRecoveryCodePage()));
        public ICommand SignInCommand => new Command(() =>
        {
            SignIn();
        });
        
        
        /// <summary>
        /// methods
        /// </summary>
        private async void SignIn()
        {
            var appUserAccount = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            if ((appUserAccount.Email == Email) && (appUserAccount.PasswordHash == Password))
            {
                IsBusy = true;
                AccountBlockVisibility = false;
                SignInSignOutService.SignIn();
                await  Navigation.PushAsync(new HomePage());
                IsBusy = false;
                AccountBlockVisibility = true;
            }
            else
            {
                var wrongPassword = await DisplayAlert("Error", "Wrong Password or Email", "Try Again", "Reset Password");
                if (!wrongPassword)
                {
                    await Navigation.PushAsync(new RequestPasswordRecoveryCodePage());
                }
            }
        }

    }
}
