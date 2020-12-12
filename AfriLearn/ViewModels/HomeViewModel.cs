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
		public ICommand TermsConditionsCommand => new Command(() => NavigationService.PushAsync(new TermsAndConditionsPage()));
		public ICommand PrivacyPolicyCommand => new Command(() => NavigationService.PushAsync(new PrivacyPolicyPage()));
		public ICommand LogOutCommand => new Command(() => SignOutService.SignOut());
	}
}
