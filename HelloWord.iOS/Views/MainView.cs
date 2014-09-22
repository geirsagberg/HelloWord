
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using HelloWord.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;

namespace HelloWord.iOS
{
    public partial class MainView : MvxViewController
    {
        public MainView() : base("MainView", null)
        {
        }

        public MainViewModel MainViewModel {
            get { return ViewModel as MainViewModel; }
        }

        MvxPropertyChangedListener propertyListener;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.EdgesForExtendedLayout = UIRectEdge.None;


            var leftButton = new UIBarButtonItem {
                Title = "Get cat"
            };
            this.NavigationItem.LeftBarButtonItem = leftButton;

            var rightButton = new UIBarButtonItem {
                Title = "Get words"
            };
            this.NavigationItem.RightBarButtonItem = rightButton;

            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes, () => {
                WebView.LoadData(NSData.FromArray(MainViewModel.CatBytes), "image/gif", "utf-8", NSUrl.FromString("http://edgecats.net"));
            });

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(RandomWords).To(vm => vm.RandomWords);
            set.Bind(leftButton).To(vm => vm.FetchCatCommand);
            set.Bind(rightButton).To(vm => vm.FetchWordsCommand);
            set.Apply();
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}

