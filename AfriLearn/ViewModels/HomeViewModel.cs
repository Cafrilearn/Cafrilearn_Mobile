using AfriLearn.Services;
using AfriLearn.Views.Settings;
using AfriLearn.Views.Profile;
using System.Windows.Input;
using Xamarin.Forms;
using AfriLearn.Views;

namespace AfriLearn.ViewModels
{
    class HomeViewModel : BaseViewModel
    {					
		public ICommand NavigateToSettingsPageCommand => new Command(() =>	NavigationService.PushAsync(new  SettingPage()));
		public ICommand NavigateToProfilePageCommand => new Command(() => NavigationService.PushAsync(new  ContactProfilePage()));
		public ICommand NavigateToShareAppCommand => new Command(() =>
		 NavigationService.DisplayAlert("Hello, this is Cafrilearn.", "The app is still in tests, you will find all the information you are looking for in the complete version", "Thank you"));
		public ICommand LogOutCommand => new Command(() => SignOutService.SignOut());
	}
}
