using AfriLearnMobile.Constants;
using AfriLearnMobile.Views;
using Syncfusion.Licensing;
using Xamarin.Forms;

namespace AfriLearn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SyncfusionLicenseProvider.RegisterLicense(SyncfusionConstants.SyncKey);
            Akavache.Registrations.Start("AfriLernMobile");
            var page = new  NavigationPage(new  StartPage());
            page.BarBackgroundColor = Color.FromHex("#193566");
            MainPage = page;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
