using System;

using Xamarin.Forms;

namespace MyMenu
{
	public partial class HomePage : ContentPage
	{
		HomeViewModel vm;

		public HomePage ()
		{
			InitializeComponent ();

			vm = new HomeViewModel ();
			vm.PropertyChanged += Vm_PropertyChanged;
			BindingContext = vm;
		}

		void Favourites_Clicked (object sender, EventArgs e)
		{
			Navigation.PushAsync (new FavoritePage());
		}

		void Vm_PropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "IsBusy")
				return;

			var progress = DependencyService.Get<IProgressDisplay> ();
			if (vm.IsBusy && progress != null)
				progress.Show ();
			else
				progress.Dismiss ();
		}

		void Cart_Clicked (object sender, EventArgs e)
		{
			Navigation.PushAsync (new CheckoutPage());
		}
	}
}

