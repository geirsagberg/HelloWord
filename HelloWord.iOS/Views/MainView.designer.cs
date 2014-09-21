// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace HelloWord.iOS.Views
{
	[Register ("MainView")]
	partial class MainView
	{
		[Outlet]
		MonoTouch.UIKit.UILabel RandomWords { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView WebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (RandomWords != null) {
				RandomWords.Dispose ();
				RandomWords = null;
			}

			if (WebView != null) {
				WebView.Dispose ();
				WebView = null;
			}
		}
	}
}
