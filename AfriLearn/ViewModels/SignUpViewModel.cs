using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        /// <summary>
        /// fields
        /// </summary>
        private string confirmPassword;
        private string email;
        private string institution;
        private string password;
        private bool accountBlockVisibility = true;
        private string studyLevel;
        private bool termsAndConditions;
        private string userName;
        private AppUser appUser;
       
        /// <summary>
        /// constructors
        /// </summary>
        public SignUpViewModel()
        {
            AppUser = new AppUser();
        }

        /// <summary>
        /// properties
        /// </summary>
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


        /// <summary>
        /// commands
        /// </summary>
           
        public ICommand NavigateToTermsAndConditionsPageCommand => new Command(() => NavigationService.PushAsync(new TermsAndConditionsPage()));
        public ICommand NavigateToSignInPageCommand => new Command(() => NavigationService.PushAsync(new SignInPage()));
        public ICommand RegisterUserCommand => new Command(execute:async () =>
        {
            if (TermsAndConditions == true)
            {
                if (Email.Contains("@outlook.com") | Email.Contains("@gmail.com"))
                {
                    if (Password != ConfirmPassword)
                    {
                         NavigationService.DisplayAlert("Error", "Password and Confirm Password must match", "Okay");
                    }
                    else
                    {
                        if (InternetService.Internet())
                        {
                            IsBusy = true;
                            AccountBlockVisibility = false;
                            var user = new AppUser()
                            {
                                UserName = UserName,
                                PasswordHash = Password,
                                Email = Email,
                                StudyLevel = StudyLevel,
                                TermsAndConditionsChecked = TermsAndConditions,
                                Institution = Institution,
                                IsSignedIn = true
                            };
                            //var httpService = new HttpClientService();
                            //var registerUser = httpService.Post(appUser, "User/register");
                            //var user = JsonConvert.DeserializeObject<AppUser>(registerUser.Result);
                            await BlobCache.UserAccount.InsertObject<AppUser>("appUser", user);
                            await BlobCache.InMemory.InsertObject<AppUser>("appUser", user);
                            AzureBlobStorageService.GetAllBookNames();
                            NavigationService.PushAsync(new HomePage());
                            IsBusy = false;
                            AccountBlockVisibility = true;
                        }
                        else
                        {
                            await InternetService.NoInternet();
                        }
                    }
                }
                else
                {
                   NavigationService.DisplayAlert("Invalid Email format", "Email should contain @outlook or @gmail", "okay");
                }
            }
            else
            {
                NavigationService.DisplayAlert("Invalid", "Please Accept the Terms and Conditions first", "Okay");
            }

        }
        , canExecute : () => ValidateAppUser());
      
        /// <summary>
        /// methods
        /// </summary>
       
       private bool ValidateAppUser()
        {
            if (string.IsNullOrWhiteSpace(Email) | string.IsNullOrWhiteSpace(Password) |string.IsNullOrWhiteSpace(StudyLevel))
            {
                return false;
            }
            return  true;
        }

    }
}
