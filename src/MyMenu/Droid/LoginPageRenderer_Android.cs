//
// LoginPageRenderer_Android.cs
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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MyMenu;
using MyMenu.Droid;
using Android.App;
using Xamarin.Auth;

[assembly: ExportRenderer (typeof(HomePage), typeof(LoginPageRenderer_Android))]
namespace MyMenu.Droid
{
	public class LoginPageRenderer_Android : PageRenderer
	{
		OAuth2Authenticator authenticator;

		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);
			var activity = Context as Activity;

			authenticator = new OAuth2Authenticator (
				clientId: Helpers.ClientId, // your OAuth2 client id
				scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
				authorizeUrl: new Uri (Helpers.AuthoriseUrl), // the auth URL for the service
				redirectUrl: new Uri (Helpers.RedirectUrl)); // the redirect URL for the service

			authenticator.Completed += (sender, ee) => {
				//DismissViewController (true, null);
				if (ee.IsAuthenticated) {
					Console.WriteLine (ee.Account.Properties ["access_token"]);
				}
			};

			activity.StartActivity (authenticator.GetUI(activity));
		}		
	}
}

