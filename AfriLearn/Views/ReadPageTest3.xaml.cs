
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadPageTest3 : ContentPage
    {
        public ReadPageTest3()
        {
            InitializeComponent();
            var browser = new WebView();
            browser.Source = "https://afrilearn.blob.core.windows.net/books/SCIENCE%20-%20PLANTS.pdf?sv=2019-10-10&ss=bqtf&srt=sco&sp=rwdlacuptfx&se=2020-06-16T13:54:23Z&sig=L8U56BajpVZOdkQy0tZVz2MfWVJVue8BKtFdAonHv3E%3D&_=1592287045127";
            this.Content = browser;
        }
    }
}