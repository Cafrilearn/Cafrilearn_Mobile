using AfriLearn;
using  AfriLearn.Services;
using AfriLearn.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
		public HomeViewModel()
		{
			
		}
				
		public ICommand NavigateToSettingsPageCommand => new Command(async () =>
		{
			await App.Current.MainPage.Navigation.PushAsync(new SettingPage());
		});

		public ICommand NavigateToProfilePageCommand => new Command(async () =>
		{
			await App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
		});

		public ICommand LogOutCommand => new Command(() =>
		{
			SignInSignOutService.SignOut();
		});


	}
}
