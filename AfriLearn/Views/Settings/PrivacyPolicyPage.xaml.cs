using AfriLearn.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class  PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();            
            if (InternetService.Internet())
            {
                privacyPolicyWebView.Source = " https://reaiot.com/cafrilearn/legal/privacy_policy";
            }
        }
    }
}