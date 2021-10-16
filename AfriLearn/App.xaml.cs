using AfriLearnMobile.Constants;
using Syncfusion.Licensing;
using Xamarin.Forms;
using Akavache;
using AfriLearn.Views.Helpers;

namespace AfriLearn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SyncfusionLicenseProvider.RegisterLicense(SyncfusionConstants.SyncKey);
            Registrations.Start("AfriLernMobile");
            var page = new NavigationPage(new  StartPage())
            {
                 BarBackgroundColor = Color.FromHex("#0391CE")                 
            };
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
