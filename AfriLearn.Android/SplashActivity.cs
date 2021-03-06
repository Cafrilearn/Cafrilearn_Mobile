using AfriLearn.Droid;
using Android.App;
using Android.OS;
using System.Threading;

namespace StuSurvey.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true,  NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Thread.Sleep(1500);
            StartActivity(typeof(MainActivity));
        }
    }
}