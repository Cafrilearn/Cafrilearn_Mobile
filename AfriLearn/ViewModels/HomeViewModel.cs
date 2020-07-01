using AfriLearn.Services;
using AfriLearn.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
		public HomeViewModel()
		{
			AzureBlobStorageService.GetAllBookNames();
		}
				
		public ICommand NavigateToSettingsPageCommand => new Command(() =>
		{
			NavigationService.PushAsync(new SettingPage());
		});

		public ICommand NavigateToProfilePageCommand => new Command(() =>
		{
			NavigationService.PushAsync(new ProfilePage());
		});

		public ICommand LogOutCommand => new Command(() =>
		{
			SignInSignOutService.SignOut();
		});
	
	}
}
