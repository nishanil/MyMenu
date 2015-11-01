using System;
using System.Linq;
using Xamarin.Forms;
using MyMenu;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

[assembly: Dependency(typeof(AzureDataService))]
namespace MyMenu
{
	public class AzureDataService : IDataService
	{
	    public MobileServiceClient MobileService { get; set; }

	    readonly IMobileServiceTable<Food> foodTable;
        readonly IMobileServiceSyncTable<Food> foodsyncTable;
        #region IFoodServiceClient implementation

        public async Task<List<Food>> GetFoodItems ()
		{
			var foodItems = await foodTable.ReadAsync ();
			return foodItems.ToList();
		}

		#endregion

		public AzureDataService ()
		{
            MobileService = new MobileServiceClient(App.ApplicationURL, App.GatewayURL, App.ApplicationKey);
            foodTable = MobileService.GetTable<Food>();
		    foodsyncTable = MobileService.GetSyncTable<Food>();
		    InitializeStoreAsync().Wait();
		}

        public async Task InitializeStoreAsync()
        {
            const string localDbPath = "syncstore.db";
            var store = new MobileServiceSQLiteStore(localDbPath);
            store.DefineTable<Food>();

            // Uses the default conflict handler, which fails on conflict
            await MobileService.SyncContext.InitializeAsync(store);
        }

        public async Task SyncFoodItemsAsync()
        {
            try
            {
                await MobileService.SyncContext.PushAsync();
                await foodsyncTable.PullAsync("allFoodItems", foodsyncTable.CreateQuery());
            }
            catch (MobileServiceInvalidOperationException e)
            {
                //TODO: Insights
               Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
        }

        public async Task<List<Food>> GetFoodItemsAsync()
        {
            await SyncFoodItemsAsync();
            return await foodsyncTable.ToListAsync();
        }

	    public async Task InsertFoodItemAsync(Food food)
	    {
	        await foodsyncTable.InsertAsync(food);
	        await SyncFoodItemsAsync();
	    }

	    public async Task<bool> DeleteFoodItemAsync(Food food)
	    {
            await foodsyncTable.DeleteAsync(food);
            await SyncFoodItemsAsync();
	        return true;
	    }

	    public async Task UpdateFoodItemAsync(Food food)
	    {
            await foodsyncTable.UpdateAsync(food);
            await SyncFoodItemsAsync();
        }
	}
}

