using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinBasic.Core;

namespace BasicXamarin.Droid
{
    public class AndroidSettingsProvider : IPlatformSettingsProvider
    {
        public string ConnectionString => "Filename=" + Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "basicXamarin.db");

        public string Platform => "Android";
    }
}