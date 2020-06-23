using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Models;
using Akavache;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        private string confirmPassword;
        private  bool createAccountTextVisibility;
        private string email;
        private string institution;
        private string password;
        private bool registerAccountBlockVisibility = true;
        private string studyLevel;
        private bool termsAndConditions;
        private string userName;
      
        private AppUser appUser;
        private AppUser appUserPageTwo;
        public  List<string>  StudyLevels { get; set; }

        public SignUpViewModel()
        {
            AppUser = new AppUser();
            AppUserPageTwo = new AppUser();
            StudyLevels = GetStudyLevels();
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
        public  bool  TermsAndConditions
        {
            get { return  termsAndConditions; }
            set 
            {  
                termsAndConditions = value;
                OnPropertyChanged(nameof(TermsAndConditions));
            }
        }
        public  bool CreateAccountTextVisibility
        {
            get { return  createAccountTextVisibility; }
            set 
            {
                createAccountTextVisibility = value;
                OnPropertyChanged(nameof(CreateAccountTextVisibility));
            }
        }
        public  bool RegisterAccountBlockVisibility
        {
            get { return  registerAccountBlockVisibility; }
            set 
            {
                registerAccountBlockVisibility = value;
                OnPropertyChanged(nameof(RegisterAccountBlockVisibility));
            }
        }

        public ICommand NavigateToSignUpPageTwoCommand => 
            new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new SignUpPageTwo()));
        public ICommand NavigateToTermsAndConditionsPageCommand =>
           new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new TermsAndConditionsPage()));
        public ICommand NavigateToSignInPageCommand =>
          new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new SignInPage()));
        public ICommand StorePageOneDataCommand => new Command(execute:async () =>
        {
            if (TermsAndConditions == true)
            {
                if (Email.Contains("@outlook.com") | Email.Contains("@gmail.com"))
                {
                    if (Password != ConfirmPassword)
                    {
                        await DisplayAlert("Error", "Password and Confirm Password must match", "Okay");
                    }
                    else
                    {
                        if (Internet())
                        {
                            await StorePageOneData();
                            await App.Current.MainPage.Navigation.PushAsync(new  SignUpPageTwo());
                        }
                        else
                        {
                            await NoInternet();
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Email format", "Email should contain @outlook or @gmail", "okay");
                }
            }
            else
            {
                await  DisplayAlert("Invalid", "Please Accept the Terms and Conditions first", "Okay");
            }

        }
        , canExecute : () => ValidateAppUser());
        public ICommand  RegisterUserCommand => new Command(async () => 
           {
               if (!string.IsNullOrEmpty(StudyLevel))
               {
                   if (Internet())
                   {
                       IsBusy = true;
                       CreateAccountTextVisibility = true;
                       RegisterAccountBlockVisibility = false;
                       var appUser = await BlobCache.InMemory.GetObject<AppUser>("appUser");
                       appUser.StudyLevel = StudyLevel;
                       appUser.Institution = Institution;
                       //var httpService = new HttpClientService();
                       //var registerUser = httpService.Post(appUser, "User/register");
                       //var user = JsonConvert.DeserializeObject<AppUser>(registerUser.Result);
                       await BlobCache.UserAccount.InsertObject<AppUser>("appUser", appUser);
                       await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                       IsBusy = false;
                       CreateAccountTextVisibility = false;
                       RegisterAccountBlockVisibility = true;
                   }
                   else
                   {
                       await NoInternet();
                   }
               }
               else
               {
                   await DisplayAlert("Unspecified value", "Please select Level of Study", "Okay");
               }
              
           });
        private async Task StorePageOneData()
        {
            var signUpPageOneData = new AppUser()
            {
                UserName = UserName,
                PasswordHash = Password,
                Email = Email,
                TermsAndConditionsChecked = TermsAndConditions
            };
            await BlobCache.InMemory.InsertObject<AppUser>("appUser", signUpPageOneData);

        }
        private List<string> GetStudyLevels()
        {
            return
                new List<string>()
                {
                      "Pre Unit",
                      "Class 1",
                      "Class 2",
                      "Class 3",
                      "Class 4",
                      "Class 5",
                      "Class 6",
                      "Class 7",
                      "Class 8",
                      "Form 1",
                      "Form 2",
                      "Form 3",
                      "Form 4"
                 };
        }
        private bool ValidateAppUser()
        {
            if (string.IsNullOrWhiteSpace(UserName) | string.IsNullOrWhiteSpace(Email) |
                string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            return true;
        }

    }
}
