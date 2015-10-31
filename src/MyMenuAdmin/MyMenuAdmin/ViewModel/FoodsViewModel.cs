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
    public class FoodsViewModel : BaseViewModel
    {
        private ObservableCollection<Food> foodItems;

        public ObservableCollection<Food> FoodItems
        {
            get { return foodItems; }
            set { foodItems = value; RaisePropertyChanged(); }
        }


        IDataService azureService;
        
        public FoodsViewModel()
        {
            // Since Android tabs appear on top, the repeating title does not make any sense
            Title = Device.OnPlatform<string>("Food Items", "My Menu (Admin)","");
            azureService = DependencyService.Get<IDataService>();
            LoadFoodItems();
        }

        async void LoadFoodItems()
        {
            IsBusy = true;
            FoodItems = new ObservableCollection<Food>(await azureService.GetFoodItems());
            IsBusy = false;
        }
    }
}
