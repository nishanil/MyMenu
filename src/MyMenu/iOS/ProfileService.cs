//
// ProfileService.cs
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
using System.Threading.Tasks;
using System.IO;
using Xamarin.Auth;
using Xamarin.Social.Services;
using Xamarin.Forms;
using MyMenu.iOS;
using Newtonsoft.Json;

[assembly:Dependency (typeof(ProfileService))]
namespace MyMenu.iOS
{
	public class ProfileService : IProfileService
	{
		public ProfileService ()
		{
			account = AccountStore.Create ().FindAccountsForService ("facebook").FirstOrDefault ();
			fbService = new FacebookService {
				ClientId = AuthHelpers.ClientId
			};
		}

		public async Task<Stream> GetProfilePictureAsync ()
		{
			var request = fbService.CreateRequest ("GET", 
				              new Uri ("https://graph.facebook.com/v2.5/me/picture?width=480&height=480"), 
				              account);
			var profilePicture = await request.GetResponseAsync ();

			return profilePicture.GetResponseStream ();
		}

		public async Task<FacebookProfile> GetProfileAsync ()
		{
			var request = fbService.CreateRequest ("GET", 
				              new Uri ("https://graph.facebook.com/v2.5/me"), 
				              account);

			var profileResponse = await request.GetResponseAsync ();
			string responseString = profileResponse.GetResponseText ();

			return JsonConvert.DeserializeObject<FacebookProfile> (responseString);
		}

		readonly Account account;
		readonly FacebookService fbService;

	}
}

