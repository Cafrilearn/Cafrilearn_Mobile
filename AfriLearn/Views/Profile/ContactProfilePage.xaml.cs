using AfriLearn.Services;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views.Profile
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactProfilePage
    {
        public ContactProfilePage()
        {
            InitializeComponent();
        }
        private void EditButton_Clicked(object sender, System.EventArgs e)
        {
            NavigationService.DisplayAlert("Hello, this is Cafrilearn.", "The app is still in tests, you will find all the information you are looking for in the complete version", "Thank you");
        }
    }
}