//
// MyMenu.cs
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
using MyMenu.Helpers;

namespace MyMenu
{
	public class App : Application
	{
		public static MobileServiceClient Client { get; private set; }

		public App ()
		{
			Client = new MobileServiceClient ("https://mymenu-ea.azure-mobile.net/", 
				"MCXpcoqnEmOwkDWhoAHAOJjxQtzMUa83");

			if (string.IsNullOrEmpty (Settings.CurrentUser)) {
				MainPage = new LoginPage ();
				return;
			}

			var user = new MobileServiceUser (Settings.CurrentUser) { 
				MobileServiceAuthenticationToken = Settings.AccessToken 
			};

			Client.CurrentUser = user;

			MainPage = new NavigationPage (new HomePage ()) {
				BarBackgroundColor = Color.FromHex ("E91E63"),
				BarTextColor = Color.White
			};
		}


		public static DataManager Manager {
			get;
			set;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

