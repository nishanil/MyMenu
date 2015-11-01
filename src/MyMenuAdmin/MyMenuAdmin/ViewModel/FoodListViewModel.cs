using MyMenu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        IDataService azureService;
        
        public FoodListViewModel()
        {
            // Since Android tabs appear on top, the repeating title does not make any sense
            Title = Device.OnPlatform<string>("Food Items", "My Menu (Admin)","");
            azureService = DependencyService.Get<IDataService>();
            //LoadFoodItems();
        }

        public async void LoadFoodItems()
        {
            IsBusy = true;
            FoodItems = new ObservableCollection<Food>(await azureService.GetFoodItemsAsync());
            IsBusy = false;
        }
    }
}
