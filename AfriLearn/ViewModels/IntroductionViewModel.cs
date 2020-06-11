using AfriLearn;
using AfriLearn.ViewModels;
using AfriLearn.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearnMobile.ViewModels
{
    class IntroductionViewModel : BaseViewModel
    {
        public ICommand NavigateToIntroPageTwoCommand => 
            new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new IntroPageTwo()));
        public ICommand NavigateToIntroPageThreeCommand =>
            new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new  IntroPageThree()));
        public ICommand NavigateToIntroSignUpPageCommand =>
            new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new  SignUpPageOne()));
        public ICommand  SkipIntroCommand => 
            new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new  SignUpPageOne()));

    }
}
