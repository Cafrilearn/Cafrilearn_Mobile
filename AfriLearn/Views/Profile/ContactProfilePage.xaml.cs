using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views.Profile
{
    /// <summary>
    /// Page to show Contact profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactProfilePage
    {
        public ContactProfilePage()
        {
            InitializeComponent();
            //this.ProfileImage.Source = App.BaseImageUrl + "ProfileDemoImage.png";
        }
    }
}