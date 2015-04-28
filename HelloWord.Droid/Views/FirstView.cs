using Android.App;
using Android.OS;
using Android.Webkit;
using Cirrious.CrossCore.WeakSubscription;
using Cirrious.MvvmCross.Droid.Views;
using HelloWord.Core.ViewModels;

namespace HelloWord.Droid.Views
{
    [Activity]
    public class FirstView : MvxActivity<FirstViewModel>
    {
        private WebView webView;
        private MvxNamedNotifyPropertyChangedEventSubscription<byte[]> catSubscription;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);

            webView = FindViewById<WebView>(Resource.Id.webView);
            catSubscription = ViewModel.WeakSubscribe(() => ViewModel.CatBytes, delegate {
                webView.LoadData(ViewModel.CatImageBase64, "text/html", "utf-8");
            });
        }
    }
}