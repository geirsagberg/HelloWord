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

        public MainView() : base("MainView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            EdgesForExtendedLayout = UIRectEdge.None;

            var leftButton = new UIBarButtonItem
            {
                Title = "Get cat"
            };
            NavigationItem.LeftBarButtonItem = leftButton;

            var rightButton = new UIBarButtonItem
            {
                Title = "Get words"
            };
            NavigationItem.RightBarButtonItem = rightButton;

            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes,
                () => WebView.LoadData(NSData.FromArray(MainViewModel.CatBytes), "image/gif", "utf-8",
                    NSUrl.FromString("http://edgecats.net")));

            MvxFluentBindingDescriptionSet<MainView, MainViewModel> set =
                this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(RandomWords).To(vm => vm.RandomWords);
            set.Bind(leftButton).To(vm => vm.FetchCatCommand);
            set.Bind(rightButton).To(vm => vm.FetchWordsCommand);
            set.Apply();
        }
    }
}