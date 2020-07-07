using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearn.Views.Profile;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SettingViewModel : BaseViewModel
    {




        public ICommand EditProfileCommand => new Command(() =>
            {
                NavigationService.PushAsync(new ContactProfilePage());
            });
        public ICommand ChangePasswordCommand => new Command(() =>
        {
            NavigationService.PushAsync(new  RequestPasswordRecoveryCodePage());
        });
        public ICommand HelpCommand => new Command(() =>
        {
            NavigationService.DisplayAlert("Hello", "This page will be implemented later", "Okay");
        });
        public ICommand NavigateToTermsAndConditionsCommand => new Command(() =>
        {
            NavigationService.PushAsync(new TermsAndConditionsPage());
        });
        public ICommand PrivacyCommand => new Command(() =>
        {
            NavigationService.DisplayAlert("Hello", "This page will be implemented later", "Okay");
        });
        public ICommand  FAQCommand => new Command(() =>
        {
            NavigationService.DisplayAlert("Hello", "This page will be implemented later", "Okay");
        });
    }
}
