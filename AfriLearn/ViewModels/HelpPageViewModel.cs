using AfriLearn.Models;
using AfriLearn.Services;
using AfriLearnMobile.Models;
using Akavache;
using MvvmHelpers.Commands;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AfriLearn.ViewModels
{
    class HelpPageViewModel : BaseViewModel
    {
        #region fields
        private string typeOfHelp;
        private string description;
        private string helpLevel;
        private Help help;
        #endregion

        public HelpPageViewModel()
        {
            Help = new Help();
        }

        #region properties
        public Help Help
        {
            get { return help; }
            set
            {
                help = value;
                OnPropertyChanged(nameof(Help));
            }
        }
        public string TypeOfHelp
        {
            get { return typeOfHelp; }
            set
            {
                typeOfHelp = value;
                OnPropertyChanged(nameof(TypeOfHelp));
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
                Help = new Help()
                {
                    Description = this.Description
                };
            }
        }
        public string HelpLevel
        {
            get { return helpLevel; }
            set
            {
                helpLevel = value;
                OnPropertyChanged(nameof(HelpLevel));
            }
        }
        #endregion

        public ICommand SendHelpCommand => new Command(execute: async () => await RequestHelp(), canExecute: () => ValidateHelp());

        private async Task RequestHelp()
        {
            IsBusy = true;

            var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            var authHttpClient = new HttpClientService(appUser.AuthKey);


            var help = new Help()
            {
                HelpName = TypeOfHelp,
                Description = Description,
                Date = DateTime.Today.ToString(),
                HelpLevel = HelpLevel,
                // add whoever sends the help here.
            };

            await  authHttpClient.Post(help, "Help/add");
            NavigationService.DisplayAlert("Help", "We are glad you have requested our help, we shall get back to you soon and hope that we shall help.", "Okay");
            IsBusy = false;
        }
        private bool ValidateHelp()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                return false;
            }
            return true;
        }
    }
}

