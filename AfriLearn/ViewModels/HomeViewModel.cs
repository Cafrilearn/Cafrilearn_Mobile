using AfriLearn.Services;
using AfriLearn.Views.Settings;
using AfriLearn.Views.Profile;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
					
		public ICommand NavigateToSettingsPageCommand => new Command(() =>
		{
			NavigationService.PushAsync(new  SettingPage());
		});

		public ICommand NavigateToProfilePageCommand => new Command(() =>
		{
			NavigationService.PushAsync(new  ContactProfilePage());
		});

		public ICommand LogOutCommand => new Command(() =>
		{
			SignInSignOutService.SignOut();
		});
	
	}
}
