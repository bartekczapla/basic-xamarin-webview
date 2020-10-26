using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public class CustomWebViewClient: WebViewClient
    {
        private readonly Assembly _assembly;
        private readonly string _prefix;

        public CustomWebViewClient(Assembly assembly)
        {
            _assembly = assembly;
            _prefix = assembly.FullName.Split(',')[0].Trim() + '.' + "Frontend.";
        }

        public override WebResourceResponse ShouldInterceptRequest(Android.Webkit.WebView view, IWebResourceRequest request)
        {
            return OnRequest(request.Url.Path) ?? base.ShouldInterceptRequest(view, request);
        }

        [Obsolete]
        public override WebResourceResponse ShouldInterceptRequest(Android.Webkit.WebView view, string url)
        {
            return OnRequest(url) ?? base.ShouldInterceptRequest(view, url);
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
        }

        private WebResourceResponse OnRequest(string url)
        {
            var mime = MimeTypes.GetMimeType(url);

            if (mime == null) 
                return null;

            try
            {
                var stream = Open(url);
                return new WebResourceResponse(mime, "UTF-8", stream);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public Stream Open(string url)
        {
            url = url.TrimStart('/');
            url = url.Replace('\\', '.').Replace('/', '.');
            url = _prefix + url;

            var stream = _assembly.GetManifestResourceStream(url);
            var names = _assembly.GetManifestResourceNames();
            if (stream == null)
            {
                throw new FileNotFoundException("Could not find resource file.", url);
            }

            return stream;
        }
    }
}