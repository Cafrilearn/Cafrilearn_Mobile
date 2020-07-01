using System.Windows.Input;
using Xamarin.Forms;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.Services;
using AfriLearnMobile.Models;
using AfriLearn.Views;
using System.Threading.Tasks;

namespace AfriLearn.ViewModels
{
    class SetNewPasswordPageViewModel :  SignUpViewModel
    {
       
        public SetNewPasswordPageViewModel()
        {

        }
        
        public ICommand  SetNewPasswordComand => new Command(async() =>
        {
             SetNewPassword();
            await Task.Delay(4000);
        });
        public ICommand  SignUpCommnd => new Command(() =>
        {
            NavigationService.PushAsync(new SignUpPage());
        });

        private async void SetNewPassword()
        {  
            if (Password == ConfirmPassword)
            {
                
                if (InternetService.Internet())
                {
                    IsBusy = true;
                    var httpClientService = new HttpClientService();
                    var currentUserInfor = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
                    currentUserInfor.PasswordHash = Password;
                    await BlobCache.UserAccount.InsertObject<AppUser>("appUser", currentUserInfor);
                    //await httpClientService.UpDate(currentUserInfor, "AppUsers/" + currentUserInfor.Id);
                    NavigationService.PushAsync(new HomePage());
                    IsBusy = false;
                }
                else
                {
                    await InternetService.NoInternet();
                }
            }
            else
            {
               NavigationService.DisplayAlert("Invalid", "Password and Change Password should match", "Okay");
            }
        }
    }
}
