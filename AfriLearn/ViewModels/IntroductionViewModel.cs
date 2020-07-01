using AfriLearn.Services;
using AfriLearn.ViewModels;
using AfriLearn.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearnMobile.ViewModels
{
    class IntroductionViewModel : BaseViewModel
    {
        public ICommand NavigateToIntroPageTwoCommand => 
            new Command(() => NavigationService.PushAsync(new IntroPageTwo()));
        public ICommand NavigateToIntroPageThreeCommand =>
            new Command(() => NavigationService.PushAsync(new  IntroPageThree()));
        public ICommand NavigateSignUpPageCommand =>
            new Command(() => NavigationService.PushAsync(new  SignUpPage()));
        public ICommand  SkipIntroCommand => 
            new Command(() => NavigationService.PushAsync(new  SignUpPage()));

    }
}
