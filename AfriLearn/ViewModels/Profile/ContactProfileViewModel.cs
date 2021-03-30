using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model = AfriLearn.Models.Profile;
using Syncfusion.XForms.Border;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using AfriLearn.Services;
using AfriLearn.Views.Profile;

namespace AfriLearn.ViewModels.Profile
{
    /// <summary>
    /// ViewModel for Individual profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ContactProfileViewModel : BaseViewModel
    {
        #region Field

        private ObservableCollection<Model> profileInfo;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ContactProfileViewModel" /> class.
        /// </summary>
        public ContactProfileViewModel()
        {
            this.ProfileInfo = new ObservableCollection<Model>();

            for (var i = 0; i < 6; i++)
            {
                this.ProfileInfo.Add(new Model { ImagePath = "ProfileImage1" + i + ".png" });
            }
            
            this.EditProfileCommand = new Command(this.EditProfile);
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Gets or sets a collection of profile info.
        /// </summary>
        public ObservableCollection<Model> ProfileInfo
        {
            get
            {
                return this.profileInfo;
            }

            set
            {
                this.profileInfo = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command ProfileNameCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the edit button is clicked.
        /// </summary>
        public Command EditProfileCommand { get; set; }

       
        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the profile name is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void ProfileNameClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as SfBorder).BackgroundColor = (Color)retVal;
            await Task.Delay(100);

            Application.Current.Resources.TryGetValue("Gray-White", out var oldVal);
            (obj as SfBorder).BackgroundColor = (Color)oldVal;
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        public void EditProfile(object obj)
        {
            NavigationService.PushAsync(new EditProfilePage());
        }      

        #endregion
    }
}