using System.Windows.Input;
using Xamarin.Forms;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.Views;
using AfriLearn.Services;
using System.Threading.Tasks;

namespace AfriLearn.ViewModels
{
    class ConfirmReceivedPasswordRecoveryCodeViewModel : BaseViewModel
    {
        private  string confirmationCode;
        public ConfirmReceivedPasswordRecoveryCodeViewModel()
        {
           
        }
             
        public   string  ConfirmationCode
        {
            get { return  confirmationCode; }
            set 
            {
                confirmationCode = value;
                OnPropertyChanged(nameof(ConfirmationCode));
            }
        }
        public ICommand ConfirmReceivedPasswordRecoveryCodeCommand => new Command(async () =>
        {
            //var gottenCode = await BlobCache.InMemory.GetObject<string>("passwordresetcode");
            //if (string.Equals(ConfirmationCode, gottenCode))
            //{
            //     NavigationService.PushAsync(new SetNewPasswordPage());
            //}
            //else
            //{
            //    var notvalidCode = await NavigationService.DisplayAlert("Wrong code", "The code you are providing and" +
            //        " the code sent to your email do not match", "Re-enter code", "Request new code");
            //    if (!notvalidCode)
            //    {
            //        NavigationService.PopAsync();
            //    }
            //}
            await Task.Delay(3000);
            NavigationService.PushAsync(new PasswordResetPage());
        });   
    }
}
