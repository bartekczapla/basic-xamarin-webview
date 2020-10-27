using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;

namespace BasicXamarin.Droid
{
    public class CustomWebViewJSBridge : Java.Lang.Object
    {
        private readonly CustomWebView customWebView;
        public CustomWebViewJSBridge(CustomWebView customWebView)
        {
            this.customWebView = customWebView;
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            customWebView.InvokeAction(data);
        }
    }
}