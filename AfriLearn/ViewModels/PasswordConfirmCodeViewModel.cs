using AfriLearn.Services;
using AfriLearn.ViewModels;
using AfriLearn.Views;
using Akavache;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearnMobile.ViewModels
{
    class PasswordConfirmCodeViewModel : BaseViewModel
    {
        private string confirmationCode;
        public string ConfirmationCode
        {
            get { return confirmationCode; }
            set
            {
                confirmationCode = value;
                OnPropertyChanged(nameof(ConfirmationCode));
            }
        }
        public ICommand ConfirmReceivedPasswordRecoveryCodeCommand => new Command(async () =>
        {
            var gottenCode = await BlobCache.InMemory.GetObject<string>("passwordresetcode");
            if (string.Equals(ConfirmationCode, gottenCode))
            {
                NavigationService.PushAsync(new PasswordResetPage());
                return;
            }
            var invalidCode = await NavigationService.DisplayAlert("Wrong code", "The code you are providing and the code sent to your email do not match", "Re-enter code", "Request new code");
            if (!invalidCode)
            {
                NavigationService.PopAsync();
            }
        });
    }
}
