//
// IFoodServiceClient.cs
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
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;

namespace MyMenu
{

	public interface IAzureClient
	{
		MobileServiceClient MobileService{ get; }
	}

	public interface IDataService
	{
		Task<List<Food>> GetFoodItems ();

		Task InitializeStoreAsync ();

		#region methods specific to offline data sync

		#region Food

		Task<List<Food>> GetFoodItemsAsync ();

		Task InsertFoodItemAsync (Food food);

		Task<bool> DeleteFoodItemAsync (Food food);

		Task UpdateFoodItemAsync (Food food);

		#endregion

		#region Order

		Task<List<Order>> GetOrdersAsync ();

		Task InsertOrderAsync (Order food);

		Task<bool> DeleteOrderAsync (Order food);

		Task UpdateOrderAsync (Order food);

		#endregion

		#region Order Detail

		Task<List<OrderDetail>> GetOrderDetailsAsync ();

		Task InsertOrderDetailAsync (OrderDetail orderDetail);

		Task<bool> DeleteOrderDetailAsync (OrderDetail orderDetail);

		Task UpdateOrderDetailAsync (OrderDetail orderDetail);

		#endregion

		#endregion


		Task<List<FavoriteItem>> GetUserFavoritesAsync ();
		Task<RecordStatus> SaveFavorite (FavoriteItem item);
		Task SyncFavoriteItemsAysnc ();

		Task InsertCouponAsync (Coupon coupon);
		Task<List<Coupon>> GetCouponsAsync ();
	}
}

