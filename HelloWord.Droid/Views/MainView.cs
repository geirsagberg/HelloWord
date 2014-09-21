using Android.App;
using Android.OS;
using Android.Util;
using Android.Webkit;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.Extensions;
using HelloWord.ViewModels;

namespace HelloWord.Droid.Views
{
    [Activity(Label = "Cats and words")]
    public class MainView : MvxActivity
    {
        private MvxPropertyChangedListener propertyListener;
        private WebView webView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);

            webView = FindViewById<WebView>(Resource.Id.webView1);
            var obj = FindViewById(Resource.Id.webView1);
        }

        public MainViewModel MainViewModel { get { return ViewModel as MainViewModel; } }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes, () =>
            {
                string base64Image = Base64.EncodeToString(MainViewModel.CatBytes, Base64Flags.Default);
                var html = "<img src='data:image/gif;base64,{0}' />".FormatWith(base64Image);
                webView.LoadData(html, "text/html", "utf-8");
            });
        }
    }
}