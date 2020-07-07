using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using System.Reactive.Linq;

namespace AfriLearn.Services
{
    static class SignInSignOutService
    {
      
        public static async void SignOut()
        {
            var  appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            appUser.IsSignedIn = false;
            await BlobCache.UserAccount.InsertObject<AppUser>("appUser", appUser);
            await BlobCache.InMemory.InsertObject<AppUser>("appUser",  appUser);
            await App.Current.MainPage.Navigation.PushAsync(new SignInPage());
        }
        public static async void SignIn()
        {
            var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            appUser.IsSignedIn = true;
            await BlobCache.UserAccount.InsertObject<AppUser>("appUser", appUser);
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
       
    }
}
