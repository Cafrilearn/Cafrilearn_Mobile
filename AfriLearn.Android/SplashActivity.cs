using AfriLearn.Droid;
using Android.App;
using Android.OS;

namespace StuSurvey.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true,  NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(1500);
            StartActivity(typeof(MainActivity));
        }
    }
}