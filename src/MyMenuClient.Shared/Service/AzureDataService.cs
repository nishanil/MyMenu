//using System;
//using System.Linq;
//using Xamarin.Forms;
//using MyMenu;
//using Microsoft.WindowsAzure.MobileServices;
//using Microsoft.WindowsAzure.MobileServices.Sync;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Diagnostics;
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
////using MyMenu.Helpers;

////[assembly: Dependency (typeof(AzureDataService))]
//namespace MyMenu
//{
//	public class AzureDataService //: IDataService
//	{
//        public static readonly string ApplicationURL = @"https://mymenu-demo.azurewebsites.net";

//        public MobileServiceClient MobileService { get; set; }

//		readonly IMobileServiceTable<Food> foodTable;
//		readonly IMobileServiceTable<Coupon> couponTable;

//		readonly IMobileServiceSyncTable<Food> foodsyncTable;
//		readonly IMobileServiceSyncTable<Order> ordersyncTable;
//		readonly IMobileServiceSyncTable<OrderDetail> orderDetailsyncTable;
//		readonly IMobileServiceSyncTable<FavoriteItem> favoritesyncTable;


//		#region IFoodServiceClient implementation

//		public async Task<List<Food>> GetFoodItems ()
//		{
//			var foodItems = await foodTable.ReadAsync ();
//			return foodItems.ToList ();
//		}

//		#endregion

//		public AzureDataService ()
//		{
//			MobileService = new MobileServiceClient (ApplicationURL);
//			foodTable = MobileService.GetTable<Food> ();
//			couponTable = MobileService.GetTable<Coupon> ();

//			foodsyncTable = MobileService.GetSyncTable<Food> ();
//			ordersyncTable = MobileService.GetSyncTable<Order> ();
//			orderDetailsyncTable = MobileService.GetSyncTable<OrderDetail> ();
//			favoritesyncTable = MobileService.GetSyncTable<FavoriteItem> ();

//			InitializeStoreAsync ().Wait();
//		}

//		public async Task InitializeStoreAsync ()
//		{
//			const string localDbPath = "syncstore.db";
//			var store = new MobileServiceSQLiteStore (localDbPath);
//			store.DefineTable<Food> ();
//			store.DefineTable<Order> ();
//			store.DefineTable<OrderDetail> ();
//			store.DefineTable<FavoriteItem> ();
//			// Uses the default conflict handler, which fails on conflict
//			await MobileService.SyncContext.InitializeAsync (store);
//		}

//		#region Admin App Offline Sync Implementation

//		public async Task SyncFoodItemsAsync ()
//		{
//			try {
//				await MobileService.SyncContext.PushAsync ();
//				await foodsyncTable.PullAsync ("allFoodItems", foodsyncTable.CreateQuery ());
//			} catch (MobileServiceInvalidOperationException e) {
//				//TODO: Insights
//				Debug.WriteLine (@"Sync Failed: {0}", e.Message);
//			}
//		}

//		public async Task<List<Coupon>> GetCouponsAsync ()
//		{
//			try {
//				return await couponTable.ToListAsync ();
//			} catch (Exception ex) {
//				Debug.WriteLine (@"Failed: {0}", ex.Message);
//			}

//			return null;
//		}

//		public async Task InsertCouponAsync (Coupon coupon)
//		{
//			try {
//				await couponTable.InsertAsync (coupon);
//				Debug.WriteLine ("Coupon ID: {0}", coupon.Id);
//			} catch (Exception ex) {
//				Debug.WriteLine (ex.ToString ());
//			}
//		}

//		public async Task<List<FavoriteItem>> GetUserFavoritesAsync ()
//		{
//			try {
//				//await SyncTableAsync ();
//				var favourites = await favoritesyncTable.ReadAsync ();

//				return favourites.ToList ();

//			} catch (Exception ex) {
//				Debug.WriteLine (ex.ToString ());
//			}

//			return null;		
//		}

//		public async Task SyncFavoriteItemsAysnc ()
//		{
//			try {
//				await MobileService.SyncContext.PushAsync ();

//				var userId = Settings.CurrentUserId;
//				await favoritesyncTable.PullAsync ("favourites" + userId, 
//					favoritesyncTable.Where (p => p.UserId == userId));
				
//			} catch (MobileServiceInvalidOperationException e) {
//				//TODO: Insights
//				Debug.WriteLine (@"Sync Failed: {0}", e.Message);
//			}
//		}

//		public async Task<RecordStatus> SaveFavorite (FavoriteItem item)
//		{
//			var currentItem = await favoritesyncTable.ReadAsync ();
//			var foodItem = currentItem.FirstOrDefault (p => p.FoodItemId == item.FoodItemId
//			               && p.UserId == item.UserId);
//			if (foodItem == null) {
//				await favoritesyncTable.InsertAsync (item);
//				return RecordStatus.Inserted;
//			} 

//			await favoritesyncTable.DeleteAsync (foodItem);
//			return RecordStatus.Deleted;
//		}

//		public async Task<List<Food>> GetFoodItemsAsync ()
//		{
//			await SyncFoodItemsAsync ();
//			var table = await foodsyncTable.ReadAsync ();
//			return table.ToList ();
//		}

//		public async Task InsertFoodItemAsync (Food food)
//		{
//			await foodsyncTable.InsertAsync (food);
//			await SyncFoodItemsAsync ();
//		}

//		public async Task<bool> DeleteFoodItemAsync (Food food)
//		{
//			await foodsyncTable.DeleteAsync (food);
//			await SyncFoodItemsAsync ();
//			return true;
//		}

//		public async Task UpdateFoodItemAsync (Food food)
//		{
//			await foodsyncTable.UpdateAsync (food);
//			await SyncFoodItemsAsync ();
//		}

//		public async Task SyncOrdersAsync ()
//		{
//			try {
//				await MobileService.SyncContext.PushAsync ();
//				await ordersyncTable.PullAsync ("allOrders", ordersyncTable.CreateQuery ());
//			} catch (MobileServiceInvalidOperationException e) {
//				//TODO: Insights
//				Debug.WriteLine (@"Sync Failed: {0}", e.Message);
//			}
//		}

//		public async Task<List<Order>> GetOrdersAsync ()
//		{
//			await SyncOrdersAsync ();
//			return await ordersyncTable.ToListAsync ();
//		}

//		public async Task InsertOrderAsync (Order order)
//		{
//			await ordersyncTable.InsertAsync (order);
//			await SyncOrdersAsync ();
//		}

//		public async Task<bool> DeleteOrderAsync (Order order)
//		{
//			await ordersyncTable.DeleteAsync (order);
//			await SyncOrdersAsync ();
//			return true;
//		}

//		public async Task UpdateOrderAsync (Order order)
//		{
//			await ordersyncTable.UpdateAsync (order);
//			await SyncOrdersAsync ();
//		}

//		public async Task SyncOrderDetailsAsync ()
//		{
//			try {
//				await MobileService.SyncContext.PushAsync ();
//				await orderDetailsyncTable.PullAsync ("allOrderDetails", orderDetailsyncTable.CreateQuery ());
//			} catch (MobileServiceInvalidOperationException e) {
//				//TODO: Insights
//				Debug.WriteLine (@"Sync Failed: {0}", e.Message);
//			}
//		}

//		public async Task<List<OrderDetail>> GetOrderDetailsAsync ()
//		{
//			await SyncOrderDetailsAsync ();
//			return await orderDetailsyncTable.ToListAsync ();
//		}

//		public async Task InsertOrderDetailAsync (OrderDetail orderDetail)
//		{
//			await orderDetailsyncTable.InsertAsync (orderDetail);
//			await SyncOrderDetailsAsync ();
//		}

//		public async Task<bool> DeleteOrderDetailAsync (OrderDetail orderDetail)
//		{
//			await orderDetailsyncTable.DeleteAsync (orderDetail);
//			await SyncOrderDetailsAsync ();
//			return true;
//		}

//		public async Task UpdateOrderDetailAsync (OrderDetail orderDetail)
//		{
//			await orderDetailsyncTable.UpdateAsync (orderDetail);
//			await SyncOrderDetailsAsync ();
//		}

//		public async  Task<Coupon> GetCouponAsync(string code){
//			if (string.IsNullOrEmpty(code)) {
//				return null;
//			}

//			code = code.ToLower ();
//			var coupons = await couponTable.Where (p => p.Code.ToLower() == code).ToEnumerableAsync ();
//			return coupons.FirstOrDefault ();
//		}

//		#endregion
//	}
//}
	