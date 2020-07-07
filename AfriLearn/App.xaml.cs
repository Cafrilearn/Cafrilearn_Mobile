using AfriLearn.Services;
using AfriLearn.Views;
using AfriLearnMobile.Constants;
using Syncfusion.Licensing;
using Xamarin.Forms;

namespace AfriLearn
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
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
