using System.ComponentModel;
using System.Windows;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Views;
using HelloWord.ViewModels;

namespace HelloWord.WP.Views
{
    public partial class MainView : MvxPhonePage
    {
        private MvxPropertyChangedListener propertyListener;

        public MainView()
        {
            InitializeComponent();
        }

        private void Browser_OnLoaded(object sender, RoutedEventArgs e)
        {
            propertyListener = new MvxPropertyChangedListener(MainViewModel);
            propertyListener.Listen(() => MainViewModel.CatBytes, () => Browser.NavigateToString(MainViewModel.CatImageBase64));
        }

        public MainViewModel MainViewModel { get { return ViewModel as MainViewModel; } }
    }
}