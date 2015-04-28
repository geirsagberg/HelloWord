using System;
using Cirrious.CrossCore.WeakSubscription;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using HelloWord.Core.ViewModels;
using UIKit;

namespace HelloWord.iOS.Views
{
    [MvxFromStoryboard]
    public partial class FirstView : MvxViewController<FirstViewModel>
    {
        private MvxNamedNotifyPropertyChangedEventSubscription<byte[]> catSubscription;

        public FirstView(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var catsButton = new UIBarButtonItem { Title = "Get cats" };
            NavigationItem.LeftBarButtonItem = catsButton;
            var wordsButton = new UIBarButtonItem { Title = "Get words" };
            NavigationItem.RightBarButtonItem = wordsButton;

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(Words).To(vm => vm.Words);
            set.Bind(catsButton).To(vm => vm.FetchCatCommand);
            set.Bind(wordsButton).To(vm => vm.FetchWordsCommand);
            set.Apply();

            catSubscription = ViewModel.WeakSubscribe(() => ViewModel.CatBytes, delegate {
                using (var data = NSData.FromArray(ViewModel.CatBytes)) {
                    WebView.LoadData(data, "image/gif", "utf-8", new NSUrl("http://edgecats.net"));
                }
            });
        }
    }
}