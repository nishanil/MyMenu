//
// LoginPage.cs
//
// Author:
//       Prashant Cholachagudda <prashant@xamarin.com>
//
// Copyright (c) 2015 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;

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
				
				Settings.CurrntUserId = userRecord.Id;
			} catch (Exception ex) {
				await DisplayAlert ("Error", "We're unable to log you in at the moment. Try later!", "OK");
				System.Diagnostics.Debug.WriteLine (ex.Message);
			} finally {
				progress.Dismiss ();
			}

			Application.Current.MainPage = new NavigationPage (new HomePage ()) {
				BarBackgroundColor = Color.FromHex ("E91E63"),
				BarTextColor = Color.White
			};

			System.Diagnostics.Debug.WriteLine (user);
		}
	}
}


