using AfriLearn.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
            if (InternetService.Internet())
            {
                helpWebView.Source = "https://reaiot.com/contact";
            }
        }
    }
}