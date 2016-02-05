using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MyMenu
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel ()
		{
			Title = "Home";
			IsBusy = false;
			FoodItems = new ObservableCollection<FoodViewModel> ();
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsBusy = true;
                await FoodManager.SyncAsync();
                await FavoriteManager.SyncAsync(Settings.CurrntUserId);
                IsBusy = false;
            });

            LoadFoodItems();
		}

		public Command Refresh {
			get {
				return refresh ?? (refresh = new Command (async () => await LoadFoodItems()));
			}
		}

		Command refresh;

	    public FoodManager FoodManager { get; } = DependencyService.Get<IAzureDataManager<Food>>() as FoodManager;
        public FavoriteManager FavoriteManager { get; } = DependencyService.Get<IAzureDataManager<FavoriteItem>>() as FavoriteManager;


        async Task LoadFoodItems ()
		{
			try {
				IsBusy = true;
				var items = await FoodManager?.GetAsync();
				var favorites = await FavoriteManager?.GetAsync();

				var fooditems = from fi in items
				                join  fav in favorites on fi.Id equals fav.FoodItemId into prodGroup
				                from g in prodGroup.DefaultIfEmpty (null)
				                select new {FoodItem = fi, FavoriteItem = g};

				FoodItems.Clear ();

				foreach (var item in fooditems) {
					item.FoodItem.IsFavorite = (item.FavoriteItem != null);
					FoodItems.Add (new FoodViewModel (item.FoodItem));
				}
				
			} finally {
				IsBusy = false;
			}
		}

		public ObservableCollection<FoodViewModel> FoodItems {
			get;
			set;
		}

	}
}