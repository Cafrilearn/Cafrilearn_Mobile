using AfriLearn.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsAndConditionsPage : ContentPage
    {
        public TermsAndConditionsPage()
        {
            InitializeComponent();
            if (InternetService.Internet())
            {
                termsConditionsWebView.Source = "https://reaiot.com/cafrilearn/legal/terms_conditions";
            }
        }
    }
}