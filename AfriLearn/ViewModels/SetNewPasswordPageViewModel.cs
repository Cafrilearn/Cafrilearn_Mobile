using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using AfriLearn.Services;
using AfriLearnMobile.Models;
using AfriLearn.Views;

namespace AfriLearn.ViewModels
{
    class SetNewPasswordPageViewModel : BaseViewModel
    {
        private string newPassword;
        private string confirmNewPassword;
        public SetNewPasswordPageViewModel()
        {

        }
              
        public  string  NewPassword
        {
            get { return  newPassword; }
            set 
            {
                newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
              
            }
        }    
        public  string  ConfirmNewPassword
        {
            get { return  confirmNewPassword; }
            set 
            {
                confirmNewPassword = value;
                OnPropertyChanged(nameof(ConfirmNewPassword));
                

            }
        }

        public ICommand  SetNewPasswordComand => new Command(async () =>
        {
            await SetNewPassword();
        });
        public ICommand  SignUpCommnd => new Command(async () =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new  SignUpPageOne());
        });

        private async Task SetNewPassword()
        {
            
            if (NewPassword == ConfirmNewPassword)
            {
                
                if (Internet())
                {
                    IsBusy = true;
                    var httpClientService = new HttpClientService();
                    var currentUserInfor = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
                    currentUserInfor.PasswordHash = NewPassword;
                    await BlobCache.UserAccount.Invalidate("appUser");
                    await BlobCache.UserAccount.InsertObject<AppUser>("appUser", currentUserInfor);
                    await httpClientService.UpDate(currentUserInfor, "AppUsers/" + currentUserInfor.Id);
                    await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                    IsBusy = false;
                }
                else
                {
                    await NoInternet();
                }
            }
            else
            {
                await DisplayAlert("Invalid", "Password and Change Password should match", "Okay");
            }
        }
    }
}
