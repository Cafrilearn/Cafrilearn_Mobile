using AfriLearn.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SignInViewModel :  SignUpViewModel
    {
        public SignInViewModel()
        {

        }
        public ICommand NavigateToSignUpPageOneCommand =>
           new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new  SignUpPageOne()));
        public ICommand NavigateToForgotPasswordPage =>
          new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new  ForgotPasswordPage()));
        public ICommand SignInCommand => new Command(execute: async () =>
        {
            IsBusy = true;
            CreateAccountTextVisibility = true;
            RegisterAccountBlockVisibility = false;
            await SignIn();
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            IsBusy = false;
            CreateAccountTextVisibility = false;
            RegisterAccountBlockVisibility = true;
        }, canExecute: ()=> true);
        private async Task SignIn()
        {
            await Task.Delay(3000);
        }

    }
}
