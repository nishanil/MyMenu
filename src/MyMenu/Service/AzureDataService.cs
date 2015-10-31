using System;
using System.Linq;
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
	    public MobileServiceClient MobileService { get; set; }

	    readonly IMobileServiceTable<Food> foodTable;

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
        }

	}
}

