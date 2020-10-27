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

namespace BasicXamarin.Droid
{
    public class JavascriptValueCallback : Java.Lang.Object, IValueCallback
    {
        public Java.Lang.Object Value { get; private set; }
        readonly WeakReference<CustomWebView> Reference;

        public JavascriptValueCallback(CustomWebView customWebView)
        {
            Reference = new WeakReference<CustomWebView>(customWebView);
        }
        public void OnReceiveValue(Java.Lang.Object value)
        {
            if (Reference == null || !Reference.TryGetTarget(out CustomWebView renderer))
            {
                return;
            }
            Value = value;
        }

        public void ResetValue()
        {
            Value = null;
        }
    }
}