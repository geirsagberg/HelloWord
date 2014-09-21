using Android.App;
using Android.OS;
using Android.Webkit;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.ViewModels;

namespace HelloWord.Droid.Views
{
    [Activity(Label = "Cats and words")]
    public class MainView : MvxActivity
    {
        private MvxPropertyChangedListener propertyListener;
        private WebView webView;

        public MainViewModel MainViewModel
        {
            get { return ViewModel as MainViewModel; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);

            webView = FindViewById<WebView>(Resource.Id.webView);
            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes, () => webView.LoadData(MainViewModel.CatImageBase64, "text/html", "utf-8"));
        }
    }
}