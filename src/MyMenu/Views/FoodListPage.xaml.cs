using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyMenu
{
	public partial class FoodListPage : ContentPage
	{
		FoodListPageViewModel vm;

		public FoodListPage ()
		{
			InitializeComponent ();

			vm = new FoodListPageViewModel ();
			BindingContext = vm;
        }

		void Favourites_Clicked (object sender, EventArgs e)
		{
			Navigation.PushAsync (new FavoritePage());
		}
	    private void MenuItems_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
            ((ListView)sender).SelectedItem = null;
        }

	    private void Checkout_Clicked(object sender, EventArgs e)
	    {
            Navigation.PushAsync(new CheckoutPage());
        }
    }
}

