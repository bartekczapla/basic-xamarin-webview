using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BasicXamarin.Core;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BasicXamarin.Droid
{
    public class CustomWebView
    {
        private readonly Android.Webkit.WebView webView;

        const string JavaScriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";

        private Dictionary<string, Func<string, Task>> callbacks = new Dictionary<string, Func<string, Task>>();
        private JavascriptValueCallback valueCallback;

        public CustomWebView(Android.Webkit.WebView webView)
        {
            this.webView = webView;
            valueCallback = new JavascriptValueCallback(this);

            var customWebViewClient = new CustomWebViewClient(GetType().Assembly);
            customWebViewClient.onPageFinishedCustom += CustomWebViewClient_onPageFinishedCustom;
            webView.SetWebViewClient(customWebViewClient);

            webView.AddJavascriptInterface(new CustomWebViewJSBridge(this), "jsBridge");
            webView.ClearCache(true);
            webView.ClearHistory();
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            webView.Settings.DomStorageEnabled = true;

            global::Android.Webkit.WebView.SetWebContentsDebuggingEnabled(true);
        }

        private void CustomWebViewClient_onPageFinishedCustom()
        {
            var combinedFunctions = String.Join(" ",callbacks.Select(callback => GenerateFunctionScript(callback.Key)).Append(JavaScriptFunction));
            OnInjectJavascript(combinedFunctions);
        }

        public void LoadUrl(string path) => webView.LoadUrl(path);

        public void LoadJS(string script) => webView.LoadUrl($"javascript: {script}");

        public void InvokeAction(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return;
            }
            var action = JsonConvert.DeserializeObject<ActionEvent>(data);
            byte[] dBytes = Convert.FromBase64String(action.Data);
            action.Data = Encoding.UTF8.GetString(dBytes, 0, dBytes.Length);
            if (callbacks.ContainsKey(action.Action))
            {
                callbacks[action.Action]?.Invoke(action.Data);
            }
        }

        public async Task RegisterCallback(string name, Func<string, Task> callback)
        {
            var script = GenerateFunctionScript(name);
            await OnInjectJavascript(script);
            callbacks.Add(name, callback);
        }

        private async Task OnInjectJavascript(string script)
        {
            valueCallback.ResetValue();

            webView.EvaluateJavascript(script, valueCallback);

            await Task.Run(() =>
            {
                while (valueCallback.Value == null) { }
            });
        }

        private string GenerateFunctionScript(string name)
        {
            return $"function {name}(str){{invokeCSharpAction(\"{{'action':'{name}','data':'\"+window.btoa(str)+\"'}}\");}}";
        }
    }
}