using System;
using Xamarin.Forms;
using MyMenu;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using System.Collections.Generic;

[assembly: Dependency(typeof(AzureDataService))]
namespace MyMenu
{
	public class AzureDataService : IDataService
	{
		IMobileServiceSyncTable<Food> foodTable;

		#region IFoodServiceClient implementation

		public async Task<List<Food>> GetFoodItems ()
		{
			await foodTable.PullAsync ("allFoods", foodTable.CreateQuery ().Where( f=> f.IsEnabled == true));
			return await foodTable.ToListAsync();
		}

		#endregion

		public AzureDataService ()
		{
			foodTable = App.Client.GetSyncTable<Food> ();
		}
	}
}

