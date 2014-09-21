using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using HelloWord.ViewModels;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace HelloWord.iOS.Views
{
    public partial class MainView : MvxViewController
    {
        public MainView()
            : base("MainView", null)
        {
        }

        public MainViewModel MainViewModel {
            get { return ViewModel as MainViewModel;}
        }


        MvxPropertyChangedListener propertyListener;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            this.EdgesForExtendedLayout = UIRectEdge.None;

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(GetContentButton).To(vm => vm.FetchCatCommand);
            set.Bind(RandomWords).To(vm => vm.RandomWords);
            set.Apply();

            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes, (s, e) => {
                using (var data = NSData.FromArray(MainViewModel.CatBytes)){
//                    CatImage.Image = UIImage.LoadFromData(data);

                    WebView.LoadData(data, "image/gif", "UTF-8", new NSUrl("http://edgecats.net"));
                }
            });
//            propertyListener.Listen(() => MainViewModel.CatUrl, (s, e) => {
//                WebView.LoadRequest(NSUrlRequest.FromUrl(NSUrl.FromString(MainViewModel.CatUrl)));
//            });
        }
    }
}