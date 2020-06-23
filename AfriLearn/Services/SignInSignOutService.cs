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
            var user = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            user.IsSignedIn = false;
            await BlobCache.UserAccount.Invalidate("appUser");
            await BlobCache.UserAccount.InsertObject<AppUser>("appUser", user);
            await App.Current.MainPage.Navigation.PushAsync(new SignInPage ());
        }

    }
}
