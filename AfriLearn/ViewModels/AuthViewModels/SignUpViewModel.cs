using AfriLearn.Models;
using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using Newtonsoft.Json;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        #region fields
        private string confirmPassword;
        private string email;
        private string institution;
        private string password;
        private bool accountBlockVisibility = true;
        private string studyLevel;
        private bool termsAndConditions;
        private string userName;
        private AppUser appUser;
        #endregion
              
        public SignUpViewModel()
        {
            AppUser = new AppUser();
        }

        #region properties
        public AppUser  AppUser
        {
            get { return  appUser; }
            set
            {
                appUser = value;
                OnPropertyChanged(nameof(AppUser));
            }
        }
        public  string  UserName
        {
            get { return  userName; }
            set 
            {
                userName = value;
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
                    Email = this.Email,
                    PasswordHash = this.Password,
                    StudyLevel  = this.StudyLevel
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
                    Email = this.Email,
                    PasswordHash = this.Password,
                    StudyLevel = this.StudyLevel
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
                AppUser = new AppUser
                {
                    Email = this.Email,
                    PasswordHash = this.Password,
                    StudyLevel = this.StudyLevel
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
                
                OnPropertyChanged(nameof(Institution));
            }
        }
        public  bool  TermsAndConditions
        {
            get { return  termsAndConditions; }
            set 
            {  
                termsAndConditions = value;
                OnPropertyChanged(nameof(TermsAndConditions));
            }
        }
        public  bool AccountBlockVisibility
        {
            get { return accountBlockVisibility; }
            set 
            {
                accountBlockVisibility = value;
                OnPropertyChanged(nameof(AccountBlockVisibility));
            }
        }
        #endregion

        #region commands
        public ICommand NavigateToTermsAndConditionsPageCommand => new Command(() => NavigationService.PushAsync(new TermsAndConditionsPage()));
        public ICommand NavigateToSignInPageCommand => new Command(() => NavigationService.PushAsync(new SignInPage()));
        public ICommand RegisterUserCommand => new Command(execute: async () =>
        {
            if (TermsAndConditions != true)
            {
                NavigationService.DisplayAlert("Invalid", "Please Accept the Terms and Conditions first", "Okay");
                return;
            }           
            if (!(Email.ToLower().Contains("@outlook.com") | Email.ToLower().Contains("@gmail.com")))
            {
                NavigationService.DisplayAlert("Invalid Email format", "Email should contain @outlook.com or @gmail.com", "okay");
                return;
            }
            if (Password != ConfirmPassword)
            {
                NavigationService.DisplayAlert("Error", "Password and Confirm Password must match", "Okay");
                return;
            }
            if (!InternetService.Internet())
            {
                await InternetService.NoInternet();
                return;
            }
            IsBusy = true;
            AccountBlockVisibility = false;
            var user = new AppUser()
            {
                UserName = UserName,
                PasswordHash = Password,
                Email = Email.ToLower(),
                StudyLevel = StudyLevel,
                TermsAndConditionsChecked = TermsAndConditions,
                Institution = Institution,
                IsSignedIn = true,
                Role = "Student",
                Setting = new Setting()
                {
                    AppNotificationsOn = true,
                    MarkettingNotificationsOn = true,
                    NightModeOn = false                    
                }
            };

            var httpService = new HttpClientService();
            var response = await httpService.Post(user, "User/register");
            var registerUser = JsonConvert.DeserializeObject<AppUser>(response);
            await BlobCache.UserAccount.InsertObject("appUser", registerUser);
            NavigationService.PushAsync(new HomePage());

            IsBusy = false;
            AccountBlockVisibility = true;
        }, canExecute : () => ValidateAppUser()
        );
        #endregion
        private bool ValidateAppUser()
        {
            if (string.IsNullOrWhiteSpace(Email) | string.IsNullOrWhiteSpace(Password) | string.IsNullOrWhiteSpace(StudyLevel))
            {
                return false;
            }
            return  true;
        }
    }
}
