using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HelloWord.iOS.Views
{
    public partial class MainView : MvxViewController
    {
        private MvxPropertyChangedListener propertyListener;

        public MainViewModel MainViewModel
        {
            get { return ViewModel as MainViewModel; }
        }

        public MainView()
            : base("MainView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;

            var getCatButton = new UIBarButtonItem { Title = "Get cat" };
            NavigationItem.LeftBarButtonItem = getCatButton;
            var getWordsButton = new UIBarButtonItem { Title = "Get words" };
            NavigationItem.RightBarButtonItem = getWordsButton;

            MvxFluentBindingDescriptionSet<MainView, MainViewModel> set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(RandomWords).To(vm => vm.RandomWords);
            set.Bind(getCatButton).To(vm => vm.FetchCatCommand);
            set.Bind(getWordsButton).To(vm => vm.FetchWordsCommand);
            set.Apply();

            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes, () =>
            {
                using (NSData data = NSData.FromArray(MainViewModel.CatBytes))
                {
                    WebView.LoadData(data, "image/gif", "UTF-8", new NSUrl("http://edgecats.net"));
                }
            });
        }
    }
}