using AfriLearn.Views;
using AfriLearnMobile.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        private string confirmPassword;
        private string email;
        private string password;
        private string studyLevel;
        private string userName;
        private string institution;
        private AppUser appUser;
        private AppUser appUserPageTwo;

        public SignUpViewModel()
        {
            AppUser = new AppUser();
            AppUserPageTwo = new AppUser();
        }
       

        public  AppUser  AppUser
        {
            get { return  appUser; }
            set
            {
                appUser = value;
                OnPropertyChanged(nameof(AppUser));
            }
        }
        public  AppUser  AppUserPageTwo
        {
            get { return  appUserPageTwo; }
            set 
            {
                appUserPageTwo = value;
                OnPropertyChanged(nameof(AppUserPageTwo));
            }
        }

        public  string  UserName
        {
            get { return  userName; }
            set 
            {
                userName = value;
                AppUser = new AppUser
                {
                    UserName = this.userName,
                    Email = this.Email,
                    PasswordHash = this.Password
                };
                OnPropertyChanged(nameof(UserName));
            }
        }
        public  string  Email
        {
            get { return  email; }
            set 
            {
                email = value;
                AppUser = new AppUser
                {
                    UserName = this.userName,
                    Email = this.Email,
                    PasswordHash = this.Password
                };
                OnPropertyChanged(nameof(Email));
            }
        }
        public  string  Password
        {
            get { return  password; }
            set 
            {
                password = value;
                AppUser = new AppUser
                {
                    UserName = this.userName,
                    Email = this.Email,
                    PasswordHash = this.Password
                };
                OnPropertyChanged(nameof(Password));
            }
        }
        public  string  ConfirmPassword
        {
            get { return  confirmPassword; }
            set 
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public  string  StudyLevel
        {
            get { return  studyLevel; }
            set 
            {
                studyLevel = value;
                 AppUserPageTwo = new  AppUser
                {
                     StudyLevel = this.StudyLevel,
                     Institution = this.Institution,
                };
                OnPropertyChanged(nameof(StudyLevel));
            }
        }
        public  string  Institution
        {
            get { return  institution; }
            set 
            {
                institution = value;
                AppUserPageTwo = new AppUser
                {
                    StudyLevel = this.StudyLevel,
                    Institution = this.Institution,
                };
                OnPropertyChanged(nameof(Institution));
            }
        }


        public ICommand NavigateToSignUpPageTwoCommand => 
            new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new SignUpPageTwo()));
        public ICommand NavigateToTermsAndConditionsPageCommand =>
           new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new TermsAndConditionsPage()));
        public ICommand NavigateToSignInPageCommand =>
          new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new SignInPage()));
        public ICommand StorePageOneDataCommand =>
            new Command(async () => await StorePageOneData());
        public ICommand  RegisterUserCommand => new Command(execute: async () => 
           {
               IsBusy = true;
               await RegisterUser();
               await App.Current.MainPage.Navigation.PushAsync(new HomePage());
               IsBusy = false;
           }, canExecute : () => true);


        private async Task StorePageOneData()
        {

        }
        private async Task RegisterUser()
        {
            await Task.Delay(3000);
        }

    }
}
