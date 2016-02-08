using MyMenu;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MyMenuAdmin
{
    public class FoodListViewModel : BaseViewModel
    {
        private ObservableCollection<Food> foodItems;

        public ObservableCollection<Food> FoodItems
        {
            get { return foodItems; }
            set { foodItems = value; RaisePropertyChanged(); }
        }


        private FoodManager manager;
        
        public FoodListViewModel()
        {
            // Since Android tabs appear on top, the repeating title does not make any sense
            Title = Device.OnPlatform<string>("Food Items", "My Menu (Admin)","");
            manager = DependencyService.Get<IAzureDataManager<Food>>() as FoodManager;
            Device.BeginInvokeOnMainThread(async () => { await manager.SyncAsync(); });
        }

        public async void LoadFoodItems()
        {
            IsBusy = true;
            FoodItems = new ObservableCollection<Food>(await manager.GetAsync());
            IsBusy = false;
        }
    }
}
