using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BasicXamarin.Droid
{
    public class CustomWebView
    {
        private readonly Android.Webkit.WebView webView;

        public CustomWebView(Android.Webkit.WebView webView)
        {
            this.webView = webView;

            webView.SetWebViewClient(new CustomWebViewClient(GetType().Assembly));
            webView.ClearCache(true);
            webView.ClearHistory();
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            webView.Settings.DomStorageEnabled = true;
        }

        public void LoadUrl(string path) => webView.LoadUrl(path);
    }
}