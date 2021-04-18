using AfriLearn.Services;
using AfriLearn.Views.Settings;
using AfriLearn.Views.Profile;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AfriLearn.ViewModels
{
    class HomeViewModel : BaseViewModel
    {					
		public ICommand NavigateToSettingsPageCommand => new Command(() =>	NavigationService.PushAsync(new  SettingPage()));
		public ICommand NavigateToProfilePageCommand => new Command(() => NavigationService.PushAsync(new  ContactProfilePage()));
		public ICommand NavigateToShareAppCommand => new Command(async () =>
		 {
             await Share.RequestAsync(new  ShareTextRequest
             {
                 Title =  "Cafrilearn App",
                 Text = "https://play.google.com/store/apps/details?id=com.reaiot.cafrilearn"
              });

         });
		public ICommand LogOutCommand => new Command(() => SignOutService.SignOut());
	}
}
