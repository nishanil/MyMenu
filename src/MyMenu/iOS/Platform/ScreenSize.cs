using System;
using Xamarin.Forms;
using UIKit;
using MyMenu.iOS;

[assembly:Dependency(typeof(ScreenSize))]
namespace MyMenu.iOS
{
	public class ScreenSize : IScreenSize
	{
		#region IScreenSize implementation
		public Size GetScreenSize ()
		{
			return new Size(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
		}
		#endregion
		
	}
}

