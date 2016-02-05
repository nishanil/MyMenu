using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MyMenu;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureDataClient))]
namespace MyMenu
{
    public class AzureDataClient : IAzureClient
    {
        public static readonly string ApplicationUrl = @"https://mymenu-demo.azurewebsites.net";
        public MobileServiceSQLiteStore SQLiteStore { get; } = new MobileServiceSQLiteStore("mymenustore.db");
        public MobileServiceClient MobileService { get; } = new MobileServiceClient(ApplicationUrl);
        public async Task Init()
        {
            InitDataManagers();
            await MobileService.SyncContext.InitializeAsync(SQLiteStore, StoreTrackingOptions.AllNotifications);
            var dispose = MobileService.EventManager.Subscribe<Microsoft.WindowsAzure.MobileServices.Eventing.IMobileServiceEvent>((e) => {
                System.Diagnostics.Debug.WriteLine("Event Handled: " + e.Name);
            });
        }

        private void InitDataManagers()
        {
            DependencyService.Get<IAzureDataManager<Food>>().Init(SQLiteStore);
            DependencyService.Get<IAzureDataManager<Order>>().Init(SQLiteStore);
            DependencyService.Get<IAzureDataManager<OrderDetail>>().Init(SQLiteStore);
            DependencyService.Get<IAzureDataManager<Coupon>>().Init(SQLiteStore);
            DependencyService.Get<IAzureDataManager<FavoriteItem>>().Init(SQLiteStore);
        }
    }
}
