using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Constants;
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
            MainPage = new  NavigationPage(new SplashScreenPage());
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
