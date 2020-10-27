using BasicXamarin.Contract;
using BasicXamarin.Data;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Prism;
using Prism.Common;
using Prism.Ioc;
using System;
using System.IO;
using Xamarin.Forms;
using XamarinBasic.Core;
using Android.Support.V7.App;
using Unity;
using CommonServiceLocator;
using Unity.ServiceLocation;

namespace BasicXamarin.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());     
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.main);
            var webView = FindViewById<Android.Webkit.WebView>(Resource.Id.customWebView);
            var customWebView = new CustomWebView(webView);
            customWebView.LoadUrl("http://any/index.html");
            var container = ServiceRegistry.BuildContainer();
            ServiceRegistry.SetLocatorProvider(container);
        }
    }
}

