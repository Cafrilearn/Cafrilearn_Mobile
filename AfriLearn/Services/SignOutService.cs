using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace AfriLearn.Services
{
    partial class SignOutService : Application
    {      
        public static async void SignOut()
        {
            if (!InternetService.Internet())
            {
                await InternetService.NoInternet();
                return;
            }
            var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            appUser.IsSignedIn = false;
            var httpClient = new HttpClientService(appUser.AuthKey);
            await httpClient.UpDate(appUser, "User/update");
            await BlobCache.UserAccount.InvalidateAll();
            NavigationService.PushAsync(new SignInPage());
        }       
    }
}

