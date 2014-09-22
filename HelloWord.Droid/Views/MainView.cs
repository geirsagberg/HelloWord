using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.ViewModels;

namespace HelloWord.Droid.Views
{
    [Activity(Label = "MainView")]
    public class MainView : MvxActivity
    {
        private MvxPropertyChangedListener propertyListener;
        private WebView webView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.MainView);

            webView = FindViewById<WebView>(Resource.Id.webView);

            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes, () => webView.LoadData(MainViewModel.CatImageBase64, "text/html", "utf-8"));
        }

        public MainViewModel MainViewModel { get { return ViewModel as MainViewModel; } }
    }
}