using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;

namespace BasicXamarin.Droid
{
    [Activity(Label = "Xamarin Basic", 
              Theme = "@style/MainTheme.Splash",
              MainLauncher = true,
              NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
