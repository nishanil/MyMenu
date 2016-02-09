using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;

namespace MyMenu
{
	public class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			var primaryColour = Color.FromHex ("E91E63");

			var button = new Button { 
				Text = "LOGIN WITH FACEBOOK",
				BackgroundColor = Color.White,
				TextColor = primaryColour,
				WidthRequest = 200
			};
			button.Clicked += Button_Clicked;

			Content = new StackLayout { 
				Children = {
					new Image{ Source = "logo.png" },
					button
				},
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			BackgroundColor = Color.FromHex ("E91E63");
		}

		async void Button_Clicked (object sender, EventArgs e)
		{
			var user = await DependencyService.Get<IMobileClient> ().LoginAsync 
				(MobileServiceAuthenticationProvider.Facebook);

			Settings.CurrentUser = user.UserId;
			Settings.AccessToken = user.MobileServiceAuthenticationToken;

			var progress = DependencyService.Get<IProgressDisplay> ();
			try {
				progress.Show ();
				var table = App.Client.GetTable<User> ();
				var userRecord = new User (App.Client.CurrentUser);
				await table.InsertAsync (userRecord);
				
				Settings.CurrentUserId = userRecord.Id;
			} catch (Exception ex) {
				await DisplayAlert ("Error", "We're unable to log you in at the moment. Try later!", "OK");
				System.Diagnostics.Debug.WriteLine (ex.Message);
			} finally {
				progress.Dismiss ();
			}
		    await Task.Delay(500);
			Application.Current.MainPage = new NavigationPage (new FoodListPage ()) {
				BarBackgroundColor = Color.FromHex ("E91E63"),
				BarTextColor = Color.White
			};

			System.Diagnostics.Debug.WriteLine (user);
		}
	}
}


