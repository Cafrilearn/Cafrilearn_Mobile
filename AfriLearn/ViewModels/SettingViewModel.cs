using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearn.Views.Profile;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SettingViewModel : BaseViewModel
    {

        public ICommand EditProfileCommand => new Command(() => NavigationService.PushAsync(new ContactProfilePage()));
        public ICommand ChangePasswordCommand => new Command(() => NavigationService.PushAsync(new PasswordRequstCodePage()));
        public ICommand HelpCommand => new Command(() => NavigationService.PushAsync(new HelpPage()));
        public ICommand NavigateToTermsAndConditionsCommand => new Command(() => NavigationService.PushAsync(new TermsAndConditionsPage()));
        public ICommand PrivacyCommand => new Command(() => NavigationService.PushAsync(new PrivacyPolicyPage()));
        public ICommand  FAQCommand => new Command(() => NavigationService.PushAsync(new FAQPages()));
    }
}
