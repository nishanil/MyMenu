//
// DataManager.cs
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
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using MyMenu.Helpers;

namespace MyMenu
{
	public class DataManager
	{
		public DataManager (IMobileServiceClient client, IMobileServiceSyncTable<FavoriteItem> favoriteTable)
		{
			this.client = client;
			this.favoriteTable = favoriteTable;

			userId = Settings.CurrntUserId;
		}

		public async Task<List<FavoriteItem>> GetUserFavoritesAsync ()
		{
			try {
				//await SyncTableAsync ();
				var favourites = await favoriteTable.ReadAsync ();

				return favourites.ToList ();

			} catch (Exception ex) {
				Debug.WriteLine (ex.ToString ());
			}

			return null;		
		}

		public async Task<RecordStatus> SaveFavorite (FavoriteItem item)
		{
			var currentItem = await favoriteTable.ReadAsync ();
			var foodItem = currentItem.FirstOrDefault (p => p.FoodItemId == item.FoodItemId
			               && p.UserId == item.UserId);
			if (foodItem == null) {
				await favoriteTable.InsertAsync (item);
				return RecordStatus.Inserted;
			} 

			await favoriteTable.DeleteAsync (foodItem);
			return RecordStatus.Deleted;
		}

		public async Task SyncTableAsync ()
		{
			try {
				await client.SyncContext.PushAsync ();
				await favoriteTable.PullAsync ("favourites" + userId, 
					favoriteTable.Where (p => p.UserId == userId));

			} catch (MobileServiceInvalidOperationException ex) {
				Debug.WriteLine (@"Sync Failed: {0}", ex.Message);
			}
		}


		readonly string userId;
		readonly IMobileServiceSyncTable<FavoriteItem> favoriteTable;
		readonly IMobileServiceClient client;
	}
}

