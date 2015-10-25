//
// LoginPageRenderer.cs
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
using System.Linq;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MyMenu;
using MyMenu.iOS;
using Xamarin.Auth;
using MyMenu.Helpers;
using Xamarin.Social.Services;
using System.IO;


[assembly: ExportRenderer (typeof(HomePage), typeof(LoginPageRenderer_iOS))]
namespace MyMenu.iOS
{
	public class LoginPageRenderer_iOS : PageRenderer
	{
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (!string.IsNullOrEmpty (Settings.AccessToken)) {

				var account = AccountStore.Create ().FindAccountsForService ("facebook").FirstOrDefault ();
				var fb = new FacebookService {
					ClientId = AuthHelpers.ClientId
				};

				DownloadProfilePicture (account, fb);

				return;
			}

			var authenticator = new OAuth2Authenticator (
				                    clientId: AuthHelpers.ClientId, // your OAuth2 client id
				                    scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
				                    authorizeUrl: new Uri (AuthHelpers.AuthoriseUrl), // the auth URL for the service
				                    redirectUrl: new Uri (AuthHelpers.RedirectUrl)); // the redirect URL for the service

			authenticator.Completed += (sender, e) => {
				DismissViewController (true, null);
				const string accessTokenKey = "access_token";
				if (e.IsAuthenticated && e.Account.Properties.ContainsKey (accessTokenKey)) {
					Settings.AccessToken = e.Account.Properties [accessTokenKey];
					AccountStore.Create ().Save (e.Account, "facebook");
				}
			};

			PresentViewController (authenticator.GetUI (), true, null);

		}

		async void DownloadProfilePicture (Account account, FacebookService fb)
		{
			if (Settings.IsProfileDownloaded) {
				return;
			}

			var request = fb.CreateRequest ("GET", new Uri ("https://graph.facebook.com/v2.5/me/picture?width=480&height=480"), account);
			var profilePicture = await request.GetResponseAsync ();
			string path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "profile.jpg");
			using (var pictureStream = profilePicture.GetResponseStream ()) {
				using (var file = new FileStream (path, FileMode.Create, FileAccess.Write)) {
					using (var ms = new MemoryStream ()) {
						await pictureStream.CopyToAsync (ms);
						ms.WriteTo (file);
						Settings.IsProfileDownloaded = true;
					}
				}
			}
		}
	}
}